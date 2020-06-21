using System;
using System.ServiceModel;
using System.Transactions;
using BLL.Publication;
using Entities.PublicationEntities;
using ServiceModelEx;

namespace Manager.Publication
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ReleaseServiceInstanceOnTransactionComplete = false)]
   public class PublicationManager : WcfWrapper<BLL.Publication.Publication, BLL.Publication.IPublication>, Manager.Publication.IPublicationManager
   {
      [OperationBehavior(TransactionScopeRequired = true)]
      public void Publish(int publicationTaskID)
      {
         try
         {
            PublicationTask publicationTask = Proxy.PopulatePublicationTask(publicationTaskID);

            if(publicationTask!=null)
            {
               bool isTaskPublishable = Proxy.IsTaskPublishable(publicationTask);

               if(isTaskPublishable)
               {
                  publicationTask.Status = 1;
                  HelperClasses.Output.ThrowIfFailed(Proxy.SaveTask(publicationTask), string.Format("Could not set task {0} queued status.", publicationTaskID));
                  PublishItems(publicationTask);

                  HelperClasses.Output.ThrowIfFailed(Proxy.IsTaskPublished(publicationTask), string.Format("Unpublished items in task {0}.", publicationTaskID));
                  
                  publicationTask.Status = 2;
                  HelperClasses.Output.ThrowIfFailed(Proxy.SaveTask(publicationTask), string.Format("Could not set task {0} published status.", publicationTaskID));
               }
            }
            else
            {
                 HelperClasses.Output.ThrowIfFailed(false, "Could not populate publication task.");
            }
         }
         catch(Exception ex)
         {
            HelperClasses.Output.ThrowIfFailed(false, ex.Message);
         }
       
         //Life is good, so let transaction commit
         HelperClasses.Output.WriteMessage(string.Format("Published {0} ", publicationTaskID.ToString()));
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      public void Cancel(int publicationTaskID)
      {
         HelperClasses.Output.WriteMessage(string.Format("Cancel {0} ", publicationTaskID.ToString()));

         try
         {
            Entities.PublicationEntities.PublicationTask publicationTask = DAL.PublicationDAL.PublicationTask.GetPublicationTask(publicationTaskID);
            publicationTask.Status = -1;
            HelperClasses.Output.ThrowIfFailed(Proxy.SaveTask(publicationTask),"Could not cancel task publication.");
         }
         catch(Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }

      private void PublishItems(Entities.PublicationEntities.PublicationTask publicationTask)
      {   
         bool saved = false;

         try
         {
            foreach(Entities.PublicationEntities.PublicationItem publicationItem in publicationTask.PublicationItems)
            {
               if(publicationItem.Status == 0)
               {
                  //Put each item in the MSMQ and let MSMQ handle it.
                  //The distributor updates the publication item status, at some point in the future.
                  //The MSMQ is still transactional and automatically creates its own new transaction.
                  //But we don't want it to effect the ambient transaction, **so suppress it**.

                  using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
                  {
                     Proxies.DistributionHubClient distributionHubService = new Proxies.DistributionHubClient();
                     distributionHubService.Distribute(publicationItem.PublicationItemID);

                     publicationItem.Status = 1;
                     saved = Proxy.SaveItem(publicationItem);  //

                     if(saved)
                     {
                        HelperClasses.Output.WriteMessage(string.Format("Item {0} queued for publication.", publicationItem.PublicationItemID.ToString()));
                        scope.Complete();
                     }
                  }
               }
            }
         }
         catch(Exception ex)
         {
            HelperClasses.Output.ThrowIfFailed(false, ex.Message);
         }
      }
   }
}
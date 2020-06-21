using System;
using System.IO;
using System.ServiceModel;
using Contracts.MediaService;
using Proxies;
using ServiceModelEx;

// -1 = Cancelled, 0 = Unpublished, 1 = In queue, 2 = Published, 3 = Error

namespace Manager.DistributionHub
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ReleaseServiceInstanceOnTransactionComplete = false)]
   public class DistributionHubManager : IDistributionHubManager
   {
      [OperationBehavior(TransactionScopeRequired = true)]
      public void Distribute(int publicationItemID)
      {
         BLL.DistributionHub.IDistributionHub distributionHub = InProcFactory.CreateInstance<BLL.DistributionHub.DistributionHub, BLL.DistributionHub.IDistributionHub>();

         try
         {
            Entities.PublicationEntities.PublicationItem publicationItem = distributionHub.PopulatePublicationItem(publicationItemID);

            HelperClasses.Output.ThrowIfFailed(publicationItem != null, string.Format("Could not load item {0}", publicationItemID.ToString()));

            bool isItemPublishable = distributionHub.IsItemPublishable(publicationItem);

            if(isItemPublishable)
            {
               publicationItem.Status = 1;
               HelperClasses.Output.ThrowIfFailed(distributionHub.SaveItem(publicationItem), string.Format("Could not set queued status for item {0} published status.", publicationItemID.ToString()));

               HelperClasses.Output.ThrowIfFailed(PublishIt(publicationItemID), string.Format("Could not stream to destination: {0}", publicationItemID.ToString()));
               
               publicationItem.Status = 2;
               HelperClasses.Output.ThrowIfFailed(distributionHub.SaveItem(publicationItem), string.Format("Could not set item {0} published status.",  publicationItemID.ToString()));                     
            }
         }
         catch(Exception ex)
         {
            HelperClasses.Output.ThrowIfFailed(false, ex.Message);
         }
         finally
         {
            InProcFactory.CloseProxy(distributionHub);
         }

         //Life is good, so let the transaction commit and be published.
      }

      private bool PublishIt(int publicationItemID)
      {
         bool result = false;

         try
         {
            MediaServiceClient source = new MediaServiceClient();
            MediaServiceClient destination = new MediaServiceClient();

            Stream sourceStream = source.GetStream(publicationItemID);
            StreamParameter streamParameter = new StreamParameter();
            streamParameter.Stream = sourceStream;
            streamParameter.ID = publicationItemID;

            destination.SetStream(streamParameter);

            result = true;
         }
         catch
         {
            //HelperClasses.Output.ThrowIfFailed(false, string.Format("Publish Failed for {0}", publicationItemID));
         }

         if(result)
         {
            Console.WriteLine("Publised");
         }
         else
         {
            Console.WriteLine("Failed");
         }

         return result;
      }
   }
}

using System.ServiceModel;
using System.Transactions;
using Entities.PublicationEntities;
using System;

namespace BLL.Publication
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ReleaseServiceInstanceOnTransactionComplete = false)]
   public class Publication : BLL.Publication.IPublication
   {
      [OperationBehavior(TransactionScopeRequired = true)]
      public PublicationTask PopulatePublicationTask(int publicationTaskID)
      {
         PublicationTask publicationTask = null;
         
         using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
         {
            publicationTask = DAL.PublicationDAL.PublicationTask.GetPublicationTask(publicationTaskID);
            publicationTask.PublicationItems = DAL.PublicationDAL.PublicationItem.GetPublicationItems(publicationTask.PublicationTaskID);

            scope.Complete();
         }

         return publicationTask;
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      public bool IsTaskPublishable(PublicationTask publicationTask)
      {
         if(publicationTask == null)
         {
            return false;
         }

         if(publicationTask.Status == 0 || publicationTask.Status == 1)
         {
            return true;
         }

         return false;
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      public bool SaveTask(PublicationTask publicationTask)
      {
         bool saved = false;

         using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
         {
            saved = DAL.PublicationDAL.PublicationTask.SavePublicationTask(publicationTask);
            scope.Complete();
         }

         return saved;
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      public bool SaveItem(PublicationItem publicationItem)
      {
         #region Just a bit of output to show what's going on...

         string message = string.Empty;

         switch(publicationItem.Status)
         {
            case -1:
               message = "Cancelled: " + publicationItem.PublicationItemID.ToString();
               break;
            case 0:
               message = "Default: " + publicationItem.PublicationItemID.ToString();
               break;
            case 1:
               message = "Queued: " + publicationItem.PublicationItemID.ToString();
               break;
            case 2:
               message = "Published: " + publicationItem.PublicationItemID.ToString();
               break;
            default:
               message = "Unknown: " + publicationItem.PublicationItemID.ToString();
               break;
         }

         HelperClasses.Output.WriteMessage(message);

         #endregion

         bool saved = false;

         using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
         {
            saved = DAL.PublicationDAL.PublicationItem.SavePublicationItem(publicationItem);
            scope.Complete();
         }

         return saved;
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      public bool IsTaskPublished(PublicationTask publicationTask)
      {
         foreach(Entities.PublicationEntities.PublicationItem publicationItem in publicationTask.PublicationItems)
         {
            if(publicationItem.Status != 2) // 2 = Published
            {
               return false;
            }
         }

         return true;
      }
   }
}

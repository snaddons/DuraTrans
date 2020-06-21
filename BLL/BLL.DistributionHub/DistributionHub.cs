using System.ServiceModel;
using System.Transactions;

namespace BLL.DistributionHub
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ReleaseServiceInstanceOnTransactionComplete = false)]
   public class DistributionHub : BLL.DistributionHub.IDistributionHub
   {
      [OperationBehavior(TransactionScopeRequired = true)]
      public bool SaveItem(Entities.PublicationEntities.PublicationItem publicationItem)
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
      public Entities.PublicationEntities.PublicationItem PopulatePublicationItem(int publicationItemId)
      {
         Entities.PublicationEntities.PublicationItem publicationItem = null;
         
         using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
         {
            publicationItem = DAL.PublicationDAL.PublicationItem.GetPublicationItem(publicationItemId);

            scope.Complete();
         }

         return publicationItem;
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      public bool IsItemPublishable(Entities.PublicationEntities.PublicationItem publicationItem)
      {
         return (publicationItem.Status == 0 || publicationItem.Status == 1);
      }    
   }
}

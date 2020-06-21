using System.ServiceModel;
using Contracts.Publication;

namespace ServiceLayer.Publication
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ReleaseServiceInstanceOnTransactionComplete = false)]
   public class PublicationService : WcfWrapper<Manager.Publication.PublicationManager,Manager.Publication.IPublicationManager>, IPublicationService
   {      
      [OperationBehavior(TransactionScopeRequired = true)]
      public void Publish(int publicationTaskID)
      {
         OutputEntryPoint(publicationTaskID);

         Proxy.Publish(publicationTaskID);
      }

      [OperationBehavior(TransactionScopeRequired = true)]
      public void Cancel(int publicationTaskID)
      {
         Proxy.Cancel(publicationTaskID);  
      }

      private void OutputEntryPoint(int publicationTaskID)
      {  
         HelperClasses.Output.WriteMessage(string.Format("Receive cycle for task {0} ", publicationTaskID.ToString()));
      }
   }
}
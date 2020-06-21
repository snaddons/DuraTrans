using System.ServiceModel;
using Contracts.DistributionHub;

namespace ServiceLayer
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ReleaseServiceInstanceOnTransactionComplete = false)]
   public class DistributionHubService : WcfWrapper<Manager.DistributionHub.DistributionHubManager,Manager.DistributionHub.IDistributionHubManager>, IDistributionHubService
   {
      [OperationBehavior(TransactionScopeRequired = true)]
      public void Distribute(int publicationItemID)
      {
         OutputEntryPoint(publicationItemID);

         Proxy.Distribute(publicationItemID);
      }

       private void OutputEntryPoint(int publicationItemID)
      {  
         HelperClasses.Output.WriteMessage(string.Format("Receive cycle for item {0} ", publicationItemID.ToString()));
      }
   }
}
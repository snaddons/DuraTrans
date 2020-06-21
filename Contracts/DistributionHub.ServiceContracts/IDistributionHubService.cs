using System.ServiceModel;

namespace Contracts.DistributionHub
{
   [ServiceContract]
   public interface IDistributionHubService
   {
      [OperationContract(IsOneWay = true)]
      void Distribute(int publicationItemID);
   }
}

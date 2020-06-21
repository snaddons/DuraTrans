using System;
using System.ServiceModel;

namespace Manager.DistributionHub
{
   [ServiceContract]
   public interface IDistributionHubManager
   {
      [OperationContract]
      void Distribute(int publicationItemID);
   }
}

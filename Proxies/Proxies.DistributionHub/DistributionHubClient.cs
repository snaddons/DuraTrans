using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Proxies
{
   public partial class DistributionHubClient : ClientBase<Contracts.DistributionHub.IDistributionHubService>, Contracts.DistributionHub.IDistributionHubService
   {
      public DistributionHubClient()
      {
      }

      public DistributionHubClient(string endpointConfigurationName)
         : base(endpointConfigurationName)
      {
      }

      public void Distribute(int publicationItemID)
      {
         Channel.Distribute(publicationItemID);
      }
   }
}
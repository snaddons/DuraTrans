using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Contracts.Publication;

namespace Proxies
{
   public partial class PublicationClient : ClientBase<Contracts.Publication.IPublicationService>, Contracts.Publication.IPublicationService
   {
      public PublicationClient()
      {
      }

      public PublicationClient(string endpointConfigurationName)
         : base(endpointConfigurationName)
      {
      }

      public void Publish(int publicationTaskID)
      {
         Channel.Publish(publicationTaskID);
      }

      public void Cancel(int publicationTaskID)
      {
         Channel.Cancel(publicationTaskID);
      }
   }
}

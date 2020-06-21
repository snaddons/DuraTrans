using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.IO;
using Contracts.MediaService;

namespace Proxies
{
   public partial class MediaServiceClient : ClientBase<IMediaService>, IMediaService
   {
      public MediaServiceClient()
      {}

      public MediaServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
      {}

      public Stream GetStream(int id)
      {
         return Channel.GetStream(id);
      }

      public void SetStream(StreamParameter streamParamater)
      {
         Channel.SetStream(streamParamater);
      }
   }
}
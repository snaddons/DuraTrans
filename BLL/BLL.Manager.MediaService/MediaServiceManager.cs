using System.IO;
using System.ServiceModel;
using Contracts.MediaService;

namespace Manager.MediaService
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
   public class MediaServiceManager :  WcfStreamWrapper<BLL.MediaService.MediaService, BLL.MediaService.IMediaService>, IMediaServiceManager
   {
      public Stream GetStream(int id)
      {
         return Proxy.GetStream(id);
      }

      public void SetStream(StreamParameter streamParameter)
      {
         Proxy.SetStream(streamParameter);
      }
   }
}

using System.IO;
using System.ServiceModel;
using Contracts.MediaService;

namespace ServiceLayer.MediaStore
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
   public class MediaService : WcfStreamWrapper<Manager.MediaService.MediaServiceManager, Manager.MediaService.IMediaServiceManager>, IMediaService
   {
      public Stream GetStream(int id)
      {
         HelperClasses.Output.WriteMessage(string.Format("Receive cycle for GetStream {0} ", id.ToString()));
         return Proxy.GetStream(id);        
      }

      public void SetStream(StreamParameter streamParameter)
      {
         HelperClasses.Output.WriteMessage(string.Format("Receive cycle for SetStream {0} ", streamParameter.ID.ToString()));
         Proxy.SetStream(streamParameter);
      }
   }
}
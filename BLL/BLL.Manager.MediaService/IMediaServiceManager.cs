using System.IO;
using System.ServiceModel;
using Contracts.MediaService;

namespace Manager.MediaService
{
   [ServiceContract]
   public interface IMediaServiceManager
   {      
      [OperationContract]
      Stream GetStream(int id);

      [OperationContract]
      void SetStream(StreamParameter streamParameter);
   }
}

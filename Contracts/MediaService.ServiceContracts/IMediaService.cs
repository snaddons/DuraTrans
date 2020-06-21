using System.IO;
using System.ServiceModel;

namespace Contracts.MediaService
{
   [ServiceContract]
   public interface IMediaService
   {
      [OperationContract]
      Stream GetStream(int id);

      [OperationContract]
      void SetStream(StreamParameter streamParameter);
   }
}
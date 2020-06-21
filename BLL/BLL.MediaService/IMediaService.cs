using System;
using System.ServiceModel;
using Contracts.MediaService;

namespace BLL.MediaService
{
   [ServiceContract]
   public interface IMediaService
   {
      [OperationContract]
      System.IO.Stream GetStream(int id);

      [OperationContract]
      void SetStream(StreamParameter streamParameter);
   }
}

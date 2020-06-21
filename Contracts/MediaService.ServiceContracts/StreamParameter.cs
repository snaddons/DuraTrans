using System.ServiceModel;
using System.IO;

namespace Contracts.MediaService
{
   [MessageContract]
   public class StreamParameter
   {
      [MessageHeader]
      public int ID;

      [MessageBodyMember]
      public Stream Stream;
   }
}
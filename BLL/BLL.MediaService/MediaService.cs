using System;
using System.IO;
using System.ServiceModel;
using Contracts.MediaService;

namespace BLL.MediaService
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
   public class MediaService : IMediaService
   {
      public Stream GetStream(int id)
      {
         string filePath = string.Format("{0}{1}.wav", BLL.MediaService.Properties.Settings.Default.MediaSource, id);
         return new FileStream(filePath, FileMode.Open, FileAccess.Read);
      }

      public void SetStream(StreamParameter streamParameter)
      {
         string filePath = string.Format("{0}{1}.wav", BLL.MediaService.Properties.Settings.Default.MediaDestination, streamParameter.ID);

         try
         {
            FileStream destinationStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            ReadWriteStream(streamParameter.Stream, destinationStream);
            HelperClasses.Output.WriteMessage(string.Format("Stream complete {0} ", streamParameter.ID.ToString()));
         }
         catch
         {
            HelperClasses.Output.ThrowIfFailed(false, string.Format("Streaming problem with {0}", streamParameter.ID.ToString()));
         }
      }

      private bool ReadWriteStream(Stream readStream, Stream writeStream) 
      {
         bool result = false;

         try
         {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer,0,Length);

            // write the required bytes
            while(bytesRead > 0)
            {
               writeStream.Write(buffer,0,bytesRead);
               bytesRead = readStream.Read(buffer,0,Length);
            }

            result = true;
         }
         catch(Exception ex)
         {
            string errorMessage = ex.Message; //Log it?
         }
         finally
         {
            readStream.Close();
            writeStream.Close();
         }

         return result;
      }
   }
}

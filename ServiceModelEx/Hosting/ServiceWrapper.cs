using System.ServiceModel;

namespace ServiceModelEx.Hosting
{
  public delegate void UseServiceDelegate<T>(T proxy);

   public static class ServiceWrapper<T>
   {
      public static ChannelFactory<T> _channelFactory = new ChannelFactory<T>(""); 

      public static void Use(UseServiceDelegate<T> codeBlock)
      {
         IClientChannel proxy = (IClientChannel)_channelFactory.CreateChannel();
         bool success = false;

         try
         {
            codeBlock((T)proxy);
            proxy.Close();
            success = true;
         }
         finally
         {
            if (!success)
            {
                proxy.Abort();
            }
         }
      }
   }
}

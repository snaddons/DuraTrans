using System;
using System.ServiceModel;
using ServiceModelEx;

public abstract class WcfStreamWrapper<S,I> : IDisposable where I : class 
                                                    where S : class,I
{
   protected I Proxy
   {get;private set;}

   protected WcfStreamWrapper()
   {
      NetNamedPipeBinding binding;

      try
      {
         binding = new NetNamedPipeContextBinding("InProcFactory");
      }
      catch
      {
         binding = new NetNamedPipeContextBinding();
      }

      binding.TransferMode = TransferMode.Streamed;
      binding.MaxReceivedMessageSize = 524288;

      InProcFactory.SetBinding(binding);
      Proxy = InProcFactory.CreateInstance<S,I>(binding);
   }

   public void Dispose()
   {
      Close();
   }

   public void Close()
   {
      InProcFactory.CloseProxy(Proxy);
   }
}
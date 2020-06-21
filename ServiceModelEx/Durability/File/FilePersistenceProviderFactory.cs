// � 2009 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net


using System;
using System.Threading;
using System.ServiceModel.Persistence;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Collections.Specialized;

namespace ServiceModelEx
{
   public class FilePersistenceProviderFactory : PersistenceProviderFactory
   {
      string FileName
      {
         get;
         set;
      }
      public FilePersistenceProviderFactory() : this("Instances.bin")
      {}
      public FilePersistenceProviderFactory(string fileName)
      {
         FileName = fileName;
      }
      public FilePersistenceProviderFactory(NameValueCollection parameters) : this(parameters["fileName"])
      {}
      public override PersistenceProvider CreateProvider(Guid id)
      {
         return new FilePersistenceProvider(id,FileName);
      }
      protected override TimeSpan DefaultCloseTimeout
      {
         get
         {
            return TimeSpan.MaxValue;
         }
      }

      protected override TimeSpan DefaultOpenTimeout
      {
         get
         {
            return TimeSpan.MaxValue;
         }
      }

      protected override void OnAbort()
      {}

      protected override IAsyncResult OnBeginClose(TimeSpan timeout,AsyncCallback callback,object state)
      {
         throw new NotImplementedException();
      }

      protected override IAsyncResult OnBeginOpen(TimeSpan timeout,AsyncCallback callback,object state)
      {
         throw new NotImplementedException();
      }

      protected override void OnClose(TimeSpan timeout)
      {}

      protected override void OnEndClose(IAsyncResult result)
      {
         throw new NotImplementedException();
      }

      protected override void OnEndOpen(IAsyncResult result)
      {
         throw new NotImplementedException();
      }

      protected override void OnOpen(TimeSpan timeout)
      {}
   }
}
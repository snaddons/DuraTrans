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
   public class TransactionalInstanceProviderFactory : MemoryProviderFactory
   {
      public override PersistenceProvider CreateProvider(Guid id)
      {
         return new TransactionalInstanceProvider(id);
      }
   }
}
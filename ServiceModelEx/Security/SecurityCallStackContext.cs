// © 2008 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Threading;
using System.Runtime.Serialization;

namespace ServiceModelEx
{
   [DataContract]
   public class SecurityCallStackContext
   {
      public static SecurityCallStack Current
      {
         get
         {
            return GenericContext<SecurityCallStack>.Current.Value;
         }
         set
         {
            GenericContext<SecurityCallStack>.Current = new GenericContext<SecurityCallStack>(value);
         }
      }
   }
}

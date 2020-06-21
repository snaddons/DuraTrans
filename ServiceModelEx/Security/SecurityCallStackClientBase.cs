// © 2008 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Runtime.CompilerServices;


namespace ServiceModelEx
{
   public abstract class SecurityCallStackClientBase<T> : HeaderClientBase<T,SecurityCallStack> where T : class
   {
      protected SecurityCallStackClientBase()
      {
         InitializeCallStack();
      }
    
      public SecurityCallStackClientBase(string endpointConfigurationName) : base(endpointConfigurationName)
      {
         InitializeCallStack();
      }

      public SecurityCallStackClientBase(string endpointConfigurationName,string remoteAddress) : base(endpointConfigurationName,remoteAddress)
      {
         InitializeCallStack();
      }

      public SecurityCallStackClientBase(string endpointConfigurationName,EndpointAddress remoteAddress) : base(endpointConfigurationName,remoteAddress)
      {
         InitializeCallStack();
      }

      public SecurityCallStackClientBase(Binding binding,EndpointAddress remoteAddress) : base(binding,remoteAddress)
      {
         InitializeCallStack();
      }

      void InitializeCallStack()
      {
         if(OperationContext.Current == null)
         {
            Header = new SecurityCallStack();
         }
         else
         {
            Header = SecurityCallStackContext.Current;
         }
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      protected override object Invoke(string operation,params object[] args)
      {
         Header.AppendCall();
         return base.Invoke(operation,args);
      }
   }
}
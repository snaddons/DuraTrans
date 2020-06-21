// © 2009 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace ServiceModelEx
{
   public abstract class AsyncClientBase<T> : ClientBase<T> where T : class
   {
      protected delegate IAsyncResult BeginOperationDelegate(object[] inValues,AsyncCallback asyncCallback,object state);
      protected delegate object[] EndOperationDelegate(IAsyncResult result);

      protected class InvokeAsyncCompletedEventArgs : AsyncCompletedEventArgs
      {
         object[] m_Results;

         public InvokeAsyncCompletedEventArgs(Exception error,object userState) : base(error,false,userState)
         {}
         public object[] Results
         {
            get
            {
               return m_Results;
            }
            internal set
            {
               m_Results = value;
            }
         }
      }         

      public AsyncClientBase()
      {}

      public AsyncClientBase(string endpointConfigurationName) : base(endpointConfigurationName)
      {}

      public AsyncClientBase(string endpointConfigurationName,string remoteAddress) : base(endpointConfigurationName,remoteAddress)
      {}

      public AsyncClientBase(string endpointConfigurationName,EndpointAddress remoteAddress) : base(endpointConfigurationName,remoteAddress)
      {}

      public AsyncClientBase(Binding binding,EndpointAddress remoteAddress) : base(binding,remoteAddress)
      {}

      protected void InvokeAsync(BeginOperationDelegate beginOperationDelegate,object[] inValues,EndOperationDelegate endOperationDelegate,SendOrPostCallback competionCallback,object state)
      {
         SynchronizationContext syncContext = SynchronizationContext.Current;

         AsyncCallback completion = delegate(IAsyncResult result)
                                    {
                                       Exception error = null; 
                                       object[] results = {};
                                       try
                                       {
                                          results = endOperationDelegate(result);
                                       }
                                       catch(Exception exception)
                                       {
                                          error = exception; 
                                       }
                                       InvokeAsyncCompletedEventArgs completedEventArgs = new InvokeAsyncCompletedEventArgs(error,result.AsyncState);
                                       completedEventArgs.Results = results;
                                       if(syncContext == null)
                                       {
                                          competionCallback(completedEventArgs);
                                       }
                                       else
                                       {
                                          SendOrPostCallback send = delegate(object asyncResult)
                                                                    {
                                                                       competionCallback(asyncResult);
                                                                    };
                                          syncContext.Send(send,completedEventArgs);
                                       }
                                    };
         beginOperationDelegate(inValues,completion,state);
      }
   }
}

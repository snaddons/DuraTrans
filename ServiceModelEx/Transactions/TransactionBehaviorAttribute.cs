// © 2008 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Collections.ObjectModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Persistence;

namespace ServiceModelEx
{
   [AttributeUsage(AttributeTargets.Class)]
   public class TransactionalBehaviorAttribute : Attribute,IServiceBehavior
   {
      bool m_TransactionRequiredAllOperations;

      public TransactionalBehaviorAttribute()
      {
         m_TransactionRequiredAllOperations = true;
      }

      public TransactionalBehaviorAttribute(bool transactionRequiredAllOperations)
      {
         m_TransactionRequiredAllOperations = transactionRequiredAllOperations;
      }
      void IServiceBehavior.Validate(ServiceDescription description,ServiceHostBase host) 
      {
         ServiceBehaviorAttribute behavior = description.Behaviors.Find<ServiceBehaviorAttribute>();
         behavior.ReleaseServiceInstanceOnTransactionComplete = false;

         DurableServiceAttribute durable = new DurableServiceAttribute();
         durable.SaveStateInOperationTransaction = true;
         description.Behaviors.Add(durable);

         PersistenceProviderFactory factory = new TransactionalMemoryProviderFactory();

         PersistenceProviderBehavior persistenceBehavior = new PersistenceProviderBehavior(factory);
         description.Behaviors.Add(persistenceBehavior);

         if(m_TransactionRequiredAllOperations)
         {
            foreach(ServiceEndpoint endpoint in description.Endpoints)
            {
               foreach(OperationDescription operation in endpoint.Contract.Operations)
               {
                  operation.Behaviors.Find<OperationBehaviorAttribute>().TransactionScopeRequired = true;
               }
            }
         }
      }
      void IServiceBehavior.AddBindingParameters(ServiceDescription description,ServiceHostBase host,Collection<ServiceEndpoint> endpoints,BindingParameterCollection parameters)
      {}
      void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description,ServiceHostBase host)
      {}
   }
} 






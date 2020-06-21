using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Transactions;
using System.ServiceModel.Channels;
using System.Threading;

namespace ServiceLayer.Publication
{
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode=AddressFilterMode.Any)]
   public class PublicationPoisenService : Contracts.Publication.IPublicationService
   {
      public void Publish(int publicationTaskID)
      {
         int threadId = Thread.CurrentThread.ManagedThreadId;

         Console.WriteLine(DateTime.Now.ToLongTimeString() + " Receive cycle for task: " + publicationTaskID.ToString() + " on thread " + threadId.ToString());

         Manager.Publication.PublicationDLQManager publicationDLQManager = new Manager.Publication.PublicationDLQManager();
         publicationDLQManager.Publish(publicationTaskID);
      }

      public void Cancel(int publicationTaskID)
      {
         //No need to implement
      }
   }
}
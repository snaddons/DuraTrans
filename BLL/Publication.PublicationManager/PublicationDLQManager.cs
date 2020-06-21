using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Threading;
using System.Security.Permissions;
using System.Transactions;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Manager.Publication
{
   public class PublicationDLQManager
   {
      public void Publish(int publicationTaskID)
      {
         Console.WriteLine(DateTime.Now.ToLongTimeString() + " DLQ: " + publicationTaskID.ToString());

         MsmqMessageProperty mqProp = OperationContext.Current.IncomingMessageProperties[MsmqMessageProperty.Name] as MsmqMessageProperty;

         Console.WriteLine(DateTime.Now.ToLongTimeString() + " Message Delivery Status: {0} ", mqProp.DeliveryStatus);
         Console.WriteLine(DateTime.Now.ToLongTimeString() + " Message Delivery Failure: {0}", mqProp.DeliveryFailure);

         // Resend the message if timed out.
         if (mqProp.DeliveryFailure == DeliveryFailure.ReachQueueTimeout ||
                mqProp.DeliveryFailure == DeliveryFailure.ReceiveTimeout)
         {
            // Compensating behaviour.
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " Time To Live expired");
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " Log the message failure");

            // You can reuse the same transaction used to read the message from dlq to enqueue the message to the application queue.
            // This is exactly the same as what the client code does, 
            // just call the service again with the publicationTaskID parameter:

            Console.WriteLine(DateTime.Now.ToLongTimeString() + " Perform compensating behaviour: " + publicationTaskID.ToString());
         }
      }

      public void Cancel(int publicationTaskID)
      {
         //No need to implement
      }
   }
}
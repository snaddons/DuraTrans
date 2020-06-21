using System;
using System.ServiceModel;

namespace Manager.Publication
{
   [ServiceContract]
   public interface IPublicationManager
   {
      [OperationContract]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      void Cancel(int publicationTaskID);

      [OperationContract]
      [TransactionFlow(TransactionFlowOption.Allowed)]
      void Publish(int publicationTaskID);
   }
}

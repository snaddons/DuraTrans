using System;
using System.ServiceModel;

namespace Contracts.Publication
{
   [ServiceContract]
   public interface IPublicationService
   {
      [OperationContract(IsOneWay = true)]
      void Publish(int publicationTaskID);

      [OperationContract(IsOneWay = true)]
      void Cancel(int publicationTaskID);
   }
}


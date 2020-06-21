using System;
using System.ServiceModel;
using Entities.PublicationEntities;

namespace BLL.Publication
{
   [ServiceContract]
   public interface IPublication
   {
      [OperationContract]
      PublicationTask PopulatePublicationTask(int publicationTaskID);

      [OperationContract]
      bool IsTaskPublishable(Entities.PublicationEntities.PublicationTask publicationTask);

      [OperationContract]
      bool IsTaskPublished(Entities.PublicationEntities.PublicationTask publicationTask);

      [OperationContract]
      bool SaveTask(PublicationTask publicationTask);

      [OperationContract]
      bool SaveItem(PublicationItem publicationItem);
   }
}

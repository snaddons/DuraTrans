using System;
using System.ServiceModel;

namespace BLL.DistributionHub
{
   [ServiceContract]
   public interface IDistributionHub
   {
      [OperationContract]
      bool IsItemPublishable(Entities.PublicationEntities.PublicationItem publicationItem);

      [OperationContract]
      Entities.PublicationEntities.PublicationItem PopulatePublicationItem(int publicationItemId);

      [OperationContract]
      bool SaveItem(Entities.PublicationEntities.PublicationItem publicationItem);
   }
}

using System.Collections.Generic;

namespace Entities.PublicationEntities
{
   public class PublicationTask
   {
      public int PublicationTaskID {get;set;}
      public string Name {get;set;}
      public int Status {get;set;}
      public List<PublicationItem> PublicationItems {get;set;}
   
      public PublicationTask()
      {
      }

      public PublicationTask(int publicationTaskID, string name, int status)
      {
         PublicationTaskID = publicationTaskID;
         Name = name;
         Status = status;
      }

      public PublicationTask(int publicationTaskID, string name, int status, List<PublicationItem> publicationItems) : this(publicationTaskID, name, status)
      {
         PublicationItems = publicationItems;
      }
   }
}

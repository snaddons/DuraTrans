namespace Entities.PublicationEntities
{
   public class PublicationItem
   {
      public int PublicationItemID {get;set;}
      public int PublicationTaskID {get;set;}
      public string Name {get;set;}
      public int Status {get;set;}

      public PublicationItem()
      {
      }

      public PublicationItem(int publicationItemID, int publicationTaskID, string name, int status)
      {
         PublicationItemID = publicationItemID;
         PublicationTaskID = publicationTaskID;
         Name = name;
         Status = status;
      }
   }
}

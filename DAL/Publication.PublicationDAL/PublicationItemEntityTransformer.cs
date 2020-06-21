using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL.PublicationDAL
{
   public static class PublicationItemEntityTransformer
   {
      public static List<Entities.PublicationEntities.PublicationItem> GetPublicationTasks(List<Entities.PublicationEntities.PublicationItem> publicationItems)
      {
         IQueryable<Entities.PublicationEntities.PublicationItem> items = from publicationItem in publicationItems.AsQueryable()
                                                select new Entities.PublicationEntities.PublicationItem { PublicationItemID = publicationItem.PublicationItemID, Name = publicationItem.Name, PublicationTaskID = publicationItem.PublicationTaskID, Status = publicationItem.Status };
            
         return new List<Entities.PublicationEntities.PublicationItem>(items);
      }

      public static Converter<DataRow, Entities.PublicationEntities.PublicationItem> ConvertRowToPublicationItem = delegate(DataRow row)
      {
         Entities.PublicationEntities.PublicationItem data = new Entities.PublicationEntities.PublicationItem();
               
         data.Name = (row["Name"] == DBNull.Value) ? string.Empty : (string)row["Name"];
         data.Status = (row["Status"] == DBNull.Value) ? 0 : (int)row["status"];
         data.PublicationItemID = (row["PublicationItemID"] == DBNull.Value) ? 0 : (int)row["PublicationItemID"];
         data.PublicationTaskID = (row["PublicationTaskID"] == DBNull.Value) ? 0 : (int)row["PublicationTaskID"];

         return data;
      };
   }
}

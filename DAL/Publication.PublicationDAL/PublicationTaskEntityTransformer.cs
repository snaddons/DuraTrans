using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL.PublicationDAL
{
   public static class PublicationTaskEntityTransformer
    {
        public static List<Entities.PublicationEntities.PublicationTask> GetPublicationTasks(List<Entities.PublicationEntities.PublicationTask> publicationTasks)
        {
            IQueryable<Entities.PublicationEntities.PublicationTask> tasks = from publicationTask in publicationTasks.AsQueryable()
                                                select new Entities.PublicationEntities.PublicationTask {  PublicationTaskID = publicationTask.PublicationTaskID, Name = publicationTask.Name, Status = publicationTask.Status, PublicationItems = publicationTask.PublicationItems  };
            
           return new List<Entities.PublicationEntities.PublicationTask>(tasks);
        }

         public static Converter<DataRow, Entities.PublicationEntities.PublicationTask> ConvertRowToPublicationTask = delegate(DataRow row)
            {
               Entities.PublicationEntities.PublicationTask data = new Entities.PublicationEntities.PublicationTask();
               
               data.Name = (row["Name"] == DBNull.Value) ? string.Empty : (string)row["Name"];
               data.PublicationTaskID = (row["PublicationTaskID"] == DBNull.Value) ? 0 : (int)row["PublicationTaskID"];
               data.Status = (row["Status"] == DBNull.Value) ? 0 : (int)row["Status"];

               return data;
            };
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.PublicationDAL
{
   public static class PublicationTask
   {
      private static string _connectionString = DAL.PublicationDAL.Properties.Settings.Default.Publication;

      public static Entities.PublicationEntities.PublicationTask CreatePublicationTask(Entities.PublicationEntities.PublicationTask publicationTask)
      {
         string sql = string.Empty;

         sql = @"INSERT INTO PublicationTask (Name, Published, Status) ";
         sql += @"VALUES (@Name, @Published, @Status);";
         sql += @"SELECT CAST(SCOPE_IDENTITY() AS INT)";

         try
         {
            SqlCommand command = new SqlCommand(sql);
            command.CommandText = sql;

            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = publicationTask.Name;
            command.Parameters.Add("@Status", System.Data.SqlDbType.Int).Value = publicationTask.Status;

            DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);
            int publicationTaskID = helper.ExecuteCommandScalar(command);

            if (publicationTaskID > 0)
            {
               publicationTask.PublicationTaskID = publicationTaskID;
            }
            else
            {
               publicationTask = null;
            }
         }
         catch (Exception ex)
         {
            string errorMessage = ex.Message;
            publicationTask = null;
         }

         return publicationTask;
      }

      public static bool SavePublicationTask(Entities.PublicationEntities.PublicationTask publicationTask)
        {
            //Save and return a copy of the task
            //DAL.Helper helper = new Helper(connectionString);
            bool result = false;
           
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 10;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SavePublicationTask";

            command.Parameters.Add("@PublicationTaskID", System.Data.SqlDbType.Int).Value = publicationTask.PublicationTaskID;
            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = publicationTask.Name;
            command.Parameters.Add("@Status", System.Data.SqlDbType.Int).Value = publicationTask.Status;
           
            DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);
            result = helper.ExecuteCommand(command);

            command.Dispose();

            return result;
        }

      public static bool DeletePublicationTask(int publicationTaskID)
        {
            DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);

            string sql = string.Empty;

            sql += @"DELETE FROM PublicationTask ";
            sql += @"WHERE PublicationTaskID = @PublicationTaskID ";
            
            SqlCommand command = new SqlCommand(sql);
            command.Parameters.Add("@PublicationTaskID", System.Data.SqlDbType.Int).Value = publicationTaskID;
            
            bool result = helper.ExecuteCommand(command);
            
            command.Dispose();

            return result;
        }

      public static Entities.PublicationEntities.PublicationTask GetPublicationTask(int publicationTaskID)
        {
            DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);
            Entities.PublicationEntities.PublicationTask publicationTask = new Entities.PublicationEntities.PublicationTask();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 10;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetPublicationTask";

            command.Parameters.Add("@PublicationTaskID", System.Data.SqlDbType.Int).Value = publicationTaskID;

            DataTable dataTable = helper.GetDataTable(command);

            if (dataTable.Rows.Count == 0)
            {
                publicationTask = null;
            }
            else
            {
                publicationTask = DAL.PublicationDAL.PublicationTaskEntityTransformer.ConvertRowToPublicationTask(dataTable.Rows[0]);
            }

            return publicationTask;
        }

      public static List<Entities.PublicationEntities.PublicationTask> GetPublicationTasks()
      {
         DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);

         string sql = string.Empty;
         sql += "SELECT * FROM PublicationTask;";

         SqlCommand command = new SqlCommand(sql);

         DataTable dataTable = helper.GetDataTable(command);

         return helper.ToList(dataTable,PublicationDAL.PublicationTaskEntityTransformer.ConvertRowToPublicationTask);
       }
    }
}
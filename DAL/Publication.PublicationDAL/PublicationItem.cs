using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.PublicationDAL
{
   public static class PublicationItem
   {
      private static string _connectionString = DAL.PublicationDAL.Properties.Settings.Default.Publication;

      public static Entities.PublicationEntities.PublicationItem CreatePublicationItem(Entities.PublicationEntities.PublicationItem publicationItem)
      {
         string sql = string.Empty;

         sql = @"INSERT INTO PublicationTaskItem (PublicationTaskID, Name, Status) ";
         sql += @"VALUES (@PublicationTaskID, @Name, @Status);";
         sql += @"SELECT CAST(SCOPE_IDENTITY() AS INT)";

         try
         {
            SqlCommand command = new SqlCommand(sql);
            command.CommandText = sql;

            command.Parameters.Add("@PublicationTaskID",System.Data.SqlDbType.Int).Value = publicationItem.PublicationTaskID;
            command.Parameters.Add("@Name",System.Data.SqlDbType.NVarChar).Value = publicationItem.Name;
            command.Parameters.Add("@Status",System.Data.SqlDbType.Int).Value = publicationItem.Status;

            DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);
            int publicationItemID = helper.ExecuteCommandScalar(command);

            if(publicationItemID > 0)
            {
               publicationItem.PublicationItemID = publicationItemID;
            }
            else
            {
               publicationItem = null;
            }

         }
         catch(Exception ex)
         {
            string errorMessage = ex.Message;
            publicationItem = null;
         }

         return publicationItem;
      }

      public static bool SavePublicationItem(Entities.PublicationEntities.PublicationItem publicationItem)
      {
         bool result = false;

         SqlCommand command = new SqlCommand();
         command.CommandTimeout = 10;
         command.CommandType = CommandType.StoredProcedure;
         command.CommandText = "SavePublicationItem";

         command.Parameters.Add("@PublicationItemID",System.Data.SqlDbType.Int).Value = publicationItem.PublicationItemID;
         command.Parameters.Add("@PublicationTaskID",System.Data.SqlDbType.Int).Value = publicationItem.PublicationTaskID;
         command.Parameters.Add("@Name",System.Data.SqlDbType.NVarChar).Value = publicationItem.Name;
         command.Parameters.Add("@Status",System.Data.SqlDbType.Int).Value = publicationItem.Status;

         DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);
         result = helper.ExecuteCommand(command);

         command.Dispose();

         return result;
      }

      public static bool DeletePublicationItem(int publicationItemID)
      {
         DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);

         string sql = string.Empty;

         sql += @"DELETE FROM PublicationTaskItem ";
         sql += @"WHERE PublicationItemID = @PublicationItemID ";

         SqlCommand command = new SqlCommand(sql);
         command.CommandTimeout = 10;
         command.CommandType = CommandType.StoredProcedure;
         command.Parameters.Add("@PublicationItemID",System.Data.SqlDbType.Int).Value = publicationItemID;

         bool result = helper.ExecuteCommand(command);

         command.Dispose();

         return result;
      }

      public static Entities.PublicationEntities.PublicationItem GetPublicationItem(int publicationItemID)
      {
         DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);
         Entities.PublicationEntities.PublicationItem publicationItem = new Entities.PublicationEntities.PublicationItem();

         SqlCommand command = new SqlCommand();
         command.CommandTimeout = 10;
         command.CommandType = CommandType.StoredProcedure;
         command.CommandText = "GetPublicationItem";

         command.Parameters.Add("@PublicationItemID",System.Data.SqlDbType.Int).Value = publicationItemID;

         DataTable dataTable = helper.GetDataTable(command);

         if(dataTable.Rows.Count == 0)
         {
            publicationItem = null;
         }
         else
         {
            publicationItem = DAL.PublicationDAL.PublicationItemEntityTransformer.ConvertRowToPublicationItem(dataTable.Rows[0]);
         }

         return publicationItem;
      }

      public static List<Entities.PublicationEntities.PublicationItem> GetPublicationItems(int publicationTaskID)
      {
         DAL.Utilities.Helper helper = new DAL.Utilities.Helper(_connectionString);

         SqlCommand command = new SqlCommand();
         command.CommandTimeout = 10;
         command.CommandText = "GetPublicationItems";
         command.CommandType = CommandType.StoredProcedure;

         command.Parameters.Add("@PublicationTaskID",System.Data.SqlDbType.Int).Value = publicationTaskID;

         DataTable dataTable = helper.GetDataTable(command);

         return helper.ToList(dataTable, DAL.PublicationDAL.PublicationItemEntityTransformer.ConvertRowToPublicationItem);
      }
   }  
}

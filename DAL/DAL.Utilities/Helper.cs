using System;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Collections;
using System.Collections.Generic;

namespace DAL.Utilities
{
    [ObjectPooling(Enabled = true)]
    public class Helper : IHelper
    {
        #region Fields

        private string _connectionString = string.Empty;

        #endregion

        #region Properties

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #endregion

        #region Constructors

        public Helper()
        {
        }

        public Helper(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region Methods

        public DataSet GetDataSet(string sql)
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            IDbCommand command = new SqlCommand(sql);
            command.CommandTimeout = 30;
            command.Connection = connection;

            IDbTransaction transaction;
            transaction = connection.BeginTransaction(); //Enlist database into the transaction
            command.Transaction = transaction;

            IDbDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;

            DataSet dataSet = new DataSet();

            try
            {
                //Interact with database here, then commit the transaction
                dataAdapter.Fill(dataSet);
                transaction.Commit();
            }
            catch(Exception ex)
            {
                string errorMessage = ex.Message;
                errorMessage += "";

                transaction.Rollback(); //Abort transaction
                throw new Exception("Transaction Aborted : " + ex.Message.ToString());
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }

            return dataSet;
        }

        public DataSet GetDataSet(IDbCommand command)
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            command.CommandTimeout = 30;
            command.Connection = connection;

            IDbTransaction transaction;
            transaction = connection.BeginTransaction(); //Enlist database into the transaction
            command.Transaction = transaction;

            IDbDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;

            DataSet dataSet = new DataSet();

            try
            {
                //Interact with database here, then commit the transaction
                dataAdapter.Fill(dataSet);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //Abort transaction
                string errorMessage = ex.Message;
                errorMessage += "";
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }

            return dataSet;
        }

        public DataTable GetDataTable(string sql)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            DataTable dataTable = new DataTable();

            connection.Open();
            dataTable.Load(command.ExecuteReader());
            connection.Close();
            connection.Dispose();
            command.Dispose();

            return dataTable;
        }

        public DataTable GetDataTable(SqlCommand command)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            command.Connection = connection;
            DataTable dataTable = new DataTable();

            connection.Open();
            dataTable.Load(command.ExecuteReader());
            connection.Close();
            connection.Dispose();
            command.Dispose();

            return dataTable;
        }

        public bool ExecuteQuery(string sql)
        {
            bool result = false;

            //SqlConnection connection = new SqlConnection(m_ConnectionString);
            //SqlCommand command = new SqlCommand(sql, connection);

            //connection.Open();
            //command.ExecuteNonQuery();
            //connection.Close();

            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            IDbCommand command = new SqlCommand(sql);
            command.CommandTimeout = 30;
            command.Connection = connection;

            IDbTransaction transaction;
            transaction = connection.BeginTransaction(); //Enlist database into the transaction
            command.Transaction = transaction;

            try
            {
                //Interact with database here, then commit the transaction
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback(); //Abort transaction
                throw new Exception("Transaction Aborted.");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }

            return result;
        }

        public int ExecuteCommandScalar(IDbCommand command)
        {
            int result = 0;

            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            command.CommandTimeout = 30;
            command.Connection = connection;

            IDbTransaction transaction;
            transaction = connection.BeginTransaction(); //Enlist database into the transaction
            command.Transaction = transaction;

            try
            {
                //Interact with database here, then commit the transaction
                result = (int)command.ExecuteScalar();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //Abort transaction

                string errorMessage = ex.Message;
                errorMessage += "";
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }

            return result;
        }

        public bool ExecuteCommand(IDbCommand command)
        {
            bool result = false;

            IDbConnection connection = new SqlConnection(_connectionString);
            //connection.ConnectionTimeout = 40;
            connection.Open();

            command.CommandTimeout = 30;
            command.Connection = connection;

            //IDbTransaction transaction;
            //transaction = connection.BeginTransaction(); //Enlist database into the transaction
            //command.Transaction = transaction;
           
            try
            {
                //Interact with database here, then commit the transaction
                command.ExecuteNonQuery();
                //transaction.Commit();

                result = true;
            }
            catch (Exception ex)
            {
                //Abort transaction
               Console.WriteLine(ex.Message);
                string errorMessage = ex.Message;
                errorMessage += "";

                //transaction.Rollback();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }

            return result;
        }

        public List<T> ToList<R, T>(DataTable table, Converter<R, T> converter) where R : DataRow
        {
            if (table.Rows.Count == 0)
            {
                return new List<T>();
            }

            return Collection.JJ_ToList(table.Rows.GetEnumerator(), converter);
        }

        public T[] ToArray<R, T>(DataTable table, Converter<R, T> converter) where R : DataRow
        {
            if (table.Rows.Count == 0)
            {
                return new T[] { };
            }

            return Collection.UnsafeToArray(table.Rows, converter);
        }

        #endregion
    }
}

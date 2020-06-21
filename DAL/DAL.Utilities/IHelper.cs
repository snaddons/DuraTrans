using System;

namespace DAL.Utilities
{
    interface IHelper
    {
        bool ExecuteCommand(System.Data.IDbCommand command);
        int ExecuteCommandScalar(System.Data.IDbCommand command);
        bool ExecuteQuery(string sql);
        System.Data.DataSet GetDataSet(string sql);
        System.Data.DataSet GetDataSet(System.Data.IDbCommand command);
        System.Data.DataTable GetDataTable(string sql);
        T[] ToArray<R, T>(System.Data.DataTable table, Converter<R, T> converter) where R : System.Data.DataRow;
        System.Collections.Generic.List<T> ToList<R, T>(System.Data.DataTable table, Converter<R, T> converter) where R : System.Data.DataRow;
    }
}

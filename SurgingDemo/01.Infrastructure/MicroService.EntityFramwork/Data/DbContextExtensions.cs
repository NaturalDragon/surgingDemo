using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
namespace MicroService.EntityFramwork.Data
{
    public static class DbContextExtensions
    {
        private static void CombineParams(ref DbCommand command, Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> item in parameters)
                {
                    DbParameter parameter = command.CreateParameter();
                    parameter.ParameterName = item.Key;
                    parameter.Value = item.Value;
                    command.Parameters.Add(parameter);
                }

            }
        }

        private static DbCommand CreateCommand(DatabaseFacade facade, string sql,
            out DbConnection dbConn, Dictionary<string, object> parameters)
        {
            DbConnection conn = facade.GetDbConnection();
            dbConn = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandText = sql;
            CombineParams(ref cmd, parameters);

            return cmd;
        }

        public static DataTable SqlQuery(this DatabaseFacade facade, string sql, Dictionary<string, object> parameters)
        {
            DbCommand cmd = CreateCommand(facade, sql, out DbConnection conn, parameters);
            DbDataReader reader = cmd.ExecuteReader();
            try
            {

                DataTable dt = new DataTable();
                dt.Load(reader);

                return dt;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
        public static List<TAny> SqlQueryTAny<TAny>
            (this DatabaseFacade facade, string sql, Dictionary<string, object> parameters)
            where TAny:new()
        {
            DbCommand cmd = CreateCommand(facade, sql, out DbConnection conn, parameters);
            DbDataReader reader = cmd.ExecuteReader();
            try
            {
              var result=  EntityReader.GetEntities<TAny>(reader);
                return result;
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }
    }
}

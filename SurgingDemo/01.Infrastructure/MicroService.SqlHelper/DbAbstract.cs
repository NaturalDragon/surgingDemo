using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MicroService.SqlHelper
{
    internal abstract class DbAbstract
    {
        public DataTable SqlQuery(DatabaseFacade facade, string sql, params object[] parameters)
        {
            DbCommand cmd = CreateCommand(facade, sql, out DbConnection conn, parameters);
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }
        public DbCommand CreateCommand(DatabaseFacade facade, string sql,
          out DbConnection dbConn, params object[] parameters)
        {
            DbConnection conn = facade.GetDbConnection();
            dbConn = conn;
            conn.Open();
            DbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            CombineParams(ref cmd, parameters);

            return cmd;
        }
        public abstract void CombineParams(ref DbCommand command, params object[] parameters);
      

    }
}

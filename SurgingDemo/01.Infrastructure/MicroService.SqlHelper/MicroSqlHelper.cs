using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MicroService.SqlHelper
{
    public class MicroSqlHelper
    {
        private DbAbstract _dbAbstract;
        private DatabaseFacade _databaseFacade;
        public  MicroSqlHelper(DatabaseFacade facade)
        {
            _databaseFacade = facade;
            switch (facade.ProviderName)
            {
                case DataBaseType.MySql:
                    _dbAbstract= new MySqlDb();
                    break;
                case DataBaseType.SqlServer:
                    _dbAbstract= new SqlServerDb();
                    break;
                default:
                    throw new Exception($"没有{facade.ProviderName}相关数据库适配。");
            }
        }

        public DataTable SqlQuery(string sql, params object[] parameters)
        { 
            return _dbAbstract.SqlQuery(_databaseFacade, sql, parameters);
        }
    }
}

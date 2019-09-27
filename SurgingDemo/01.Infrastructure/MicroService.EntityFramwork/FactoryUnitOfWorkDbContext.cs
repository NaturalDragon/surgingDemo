using MicroService.Data;
using MicroService.Data.Configuration;
using MicroService.Data.Enums;
using MicroService.EntityFramwork.Mysql;
using MicroService.EntityFramwork.Oracle;
using MicroService.EntityFramwork.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.EntityFramwork
{

    public class FactoryUnitOfWorkDbContext
    {
        private DatabaseType _databaseType { get; set; }
        public FactoryUnitOfWorkDbContext()
        {
            var dataType = ConfigManager.GetValue<string>("SqlConfig:dataType");
            DatabaseType databaseType = (DatabaseType)Enum.Parse(typeof(DatabaseType), dataType);
            _databaseType = databaseType;
        }

        public DbContext GetDbContext()
        {

            switch (_databaseType)
            {
                case DatabaseType.SqlServer:
                    return new SqlServerDbContext();
                case DatabaseType.Oracle:
                    return new OracleDbContext();
                case DatabaseType.MySql:
                    return new MySqlDbContext();
                default:
                    return new SqlServerDbContext();
            }
        }

        public IServiceCollection AddDbContext(IServiceCollection services)
        {


            switch (_databaseType)
            {
                case DatabaseType.SqlServer:
                    return services.AddDbContext<SqlServerDbContext>(opt => { }, ServiceLifetime.Scoped);
                case DatabaseType.Oracle:
                    return services.AddDbContext<OracleDbContext>(opt => { }, ServiceLifetime.Scoped); ;
                case DatabaseType.MySql:
                    return services.AddDbContext<MySqlDbContext>(opt => { }, ServiceLifetime.Scoped); ;
                default:
                    return services.AddDbContext<SqlServerDbContext>(opt => { }, ServiceLifetime.Scoped); ;
            }
        }
    }
}

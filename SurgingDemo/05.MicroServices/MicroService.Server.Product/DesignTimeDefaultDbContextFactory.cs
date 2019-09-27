using MicroService.Data.Configuration;
using MicroService.EntityFramwork;
using MicroService.EntityFramwork.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MicroService.ServerHost.Product
{
   public class DesignTimeDefaultDbContextFactory: DesignTimeDbContextFactoryBase<SqlServerDbContext>
    {
        public override DbContextOptionsBuilder UseSql(DbContextOptionsBuilder builder, string connString)
        {
            string entryAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            return builder.UseSqlServer(connString, b => b.MigrationsAssembly(entryAssemblyName));
        }
        public override string GetConnectionString()
        {
            var connectionString = ConfigManager.GetValue<string>("SqlConfig:connectionString");
            return connectionString;
        }
    }
}

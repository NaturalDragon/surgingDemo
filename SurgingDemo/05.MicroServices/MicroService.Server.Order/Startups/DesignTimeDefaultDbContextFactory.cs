using MicroService.Data.Configuration;
using MicroService.EntityFramwork;
using MicroService.EntityFramwork.Mysql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MicroService.ServerHost.Order
{
    public class DesignTimeDefaultDbContextFactory : DesignTimeDbContextFactoryBase<MySqlDbContext>
    {
        public override DbContextOptionsBuilder UseSql(DbContextOptionsBuilder builder, string connString)
        {
            string entryAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            return builder.UseMySQL(connString, b => b.MigrationsAssembly(entryAssemblyName));
        }
        public override string GetConnectionString()
        {
            var connectionString = ConfigManager.GetValue<string>("SqlConfig:connectionString");
            return connectionString;
        }
    }
}

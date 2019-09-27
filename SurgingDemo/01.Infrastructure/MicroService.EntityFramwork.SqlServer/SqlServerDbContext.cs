using System;
using System.Collections.Generic;
using System.Text;
using MicroService.Data;
using MicroService.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MicroService.EntityFramwork.SqlServer
{
   public class SqlServerDbContext: UnitOfWorkDbContext
    {
        public SqlServerDbContext()
        {

        }
        public SqlServerDbContext(DbContextOptions<DbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(base.GetConnetciton());

        }

    }
}

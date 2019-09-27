using System;
using System.Collections.Generic;
using System.Text;
using MicroService.Data;
using MicroService.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MicroService.EntityFramwork.Mysql
{
    public class MySqlDbContext : UnitOfWorkDbContext
    {
        public MySqlDbContext()
        {

        }
        public MySqlDbContext(DbContextOptions<DbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
     
    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(base.GetConnetciton());

        }
       
    }
}

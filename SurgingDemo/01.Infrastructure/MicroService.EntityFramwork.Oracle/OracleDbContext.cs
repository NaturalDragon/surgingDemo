using MicroService.Data;
using MicroService.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.EntityFramwork.Oracle
{
  public  class OracleDbContext: UnitOfWorkDbContext
    {
        public OracleDbContext()
        {

        }
        public OracleDbContext(DbContextOptions<DbContext> dbContextOptions) : base(dbContextOptions)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            optionsBuilder.UseOracle(base.GetConnetciton());

        }
    }
}

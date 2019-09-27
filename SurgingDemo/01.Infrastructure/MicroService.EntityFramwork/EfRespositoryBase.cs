using LZN.Core;
using LZN.EntityFramwork.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LZN.EntityFramwork
{
    public abstract class EfRespositoryBase<TDbContext, TEntity, TPrimaryKey> : IRespositoryBase<TEntity, TPrimaryKey>
         where TEntity : class, IEntity<TPrimaryKey>
          where TDbContext : DbContext

    {
        public bool Add(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}


using MicroService.Common.Models;
using MicroService.Core;
using MicroService.Core.Data;
using MicroService.Data.Ext;
using MicroService.EntityFramwork.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.EntityFramwork
{
    public abstract class RespositoryBase<TEntity>
       : RespositoryBase<TEntity, string>, Core.IRespositoryBase<TEntity, string>
       where TEntity : class, IEntity<string>
    {
        
    }


    public abstract class RespositoryBase<TEntity, TPrimaryKey> :
         IRespositoryBase<TEntity, TPrimaryKey>
          where TEntity : class, IEntity<TPrimaryKey>
    {
       
        DbContext GetDbContext()
        {
            return new FactoryUnitOfWorkDbContext().GetDbContext();
        }
      
      


        #region private
        TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            using (var _dbContext = GetDbContext())
            {
                var entry = _dbContext.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

                return entry?.Entity as TEntity;
            }
        }

        void AttachIfNot(TEntity entity)
        {
            var _dbContext = GetDbContext();
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            _dbContext.Attach(entity);

        }

        Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        #endregion

        #region Select/Get/Query
      
        public async Task<IEnumerable<TEntity>> EntitiesByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(expression);
                return await data.ToListAsync();
            }
        }
      public  async Task<IEnumerable<TResult>> EntitiesByExpressionAsync<TResult>(Expression<Func<TEntity, bool>> expression,
        Expression<Func<TEntity, TResult>> selector)where TResult:class
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(expression).Select(selector);
                return await data.ToListAsync();
            }
        }
        public IEnumerable<TEntity> EntitiesByExpression(Expression<Func<TEntity, bool>> expression)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(expression);
                return data.ToList();
            }
        }
        public IEnumerable<TResult> EntitiesByExpression<TResult>(Expression<Func<TEntity, bool>> expression,
           Expression<Func<TEntity, TResult>> selector) where TResult : class
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(expression).Select(selector);
                return data.ToList();
            }
        }
        public  IEnumerable<IGrouping<Tkey, TEntity>>
           GroupBy<Tkey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, Tkey>> keySelector)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(expression).GroupBy(keySelector);
                return  data.ToList();
            }
        }
        public async  Task<IEnumerable<IGrouping<Tkey, TEntity>>> 
            GroupByAsync<Tkey>(Expression<Func<TEntity,bool>> expression,Expression<Func<TEntity, Tkey>> keySelector)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data =  _dbSet.Where(expression).GroupBy(keySelector);
                return await data.ToListAsync();
            }
        }
        public TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                //throw new EntityNotFoundException(typeof(TEntity), id);
                throw new Exception($"TEntity{id}");
            }

            return entity;
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitiesByExpression(predicate).Single();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
          var entities=  await EntitiesByExpressionAsync(predicate);
            return entities.Single();
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return EntitiesByExpression(CreateEqualityExpressionForId(id)).FirstOrDefault();
        }

        public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            var entities = await EntitiesByExpressionAsync(CreateEqualityExpressionForId(id));
            return entities.FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitiesByExpression(predicate).FirstOrDefault();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await EntitiesByExpressionAsync(predicate);
            return entities.FirstOrDefault();
        }

      
        public async Task<IEnumerable<TEntity>> WherePaged<TKey>(PageData pageData,
           Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> keySelector,
           bool isAsc = true) 
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var source = _dbSet.Where(e=>e.IsDelete==false);
                IOrderedQueryable<TEntity> iOrderQuery = null;
                if (isAsc)
                    iOrderQuery = source.OrderBy(keySelector);
                else
                    iOrderQuery = source.OrderByDescending(keySelector);

                pageData.Total = iOrderQuery.Where(whereLambda).Count();
                return await
                    iOrderQuery.Where(whereLambda).Skip((pageData.PageIndex - 1) * pageData.PageSize)
                     .Take(pageData.PageSize).ToListAsync();
            }
        }

       public async  Task<IEnumerable<TResult>> WherePaged<TResult, TKey>(PageData pageData,
          Expression<Func<TEntity, bool>> whereLambda
          , Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, TKey>> keySelector, bool isAsc = true) where TResult : class
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var source = _dbSet.Where(e => e.IsDelete == false);
                IOrderedQueryable<TEntity> iOrderQuery = null;
                if (isAsc)
                    iOrderQuery = source.OrderBy(keySelector);
                else
                    iOrderQuery = source.OrderByDescending(keySelector);

                pageData.Total = iOrderQuery.Where(whereLambda).Count();
                return await
                    iOrderQuery.Where(whereLambda).Skip((pageData.PageIndex - 1) * pageData.PageSize)
                     .Take(pageData.PageSize).Select(selector).ToListAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();

                return trackEnabled
                ? await Task.FromResult(_dbSet.FromSql(sql, parameters))
                : await Task.FromResult(_dbSet.FromSql(sql, parameters).AsNoTracking());
            }
        }

        public async Task<DataTable> SqlQueryDataTable(string sql, Dictionary<string, object> parameters)
        {
            using (var _dbContext = GetDbContext())
            {

                return await Task.FromResult(_dbContext.Database.SqlQuery(sql, parameters));
            }
        }
       public async Task<List<TAny>> SqlQueryEntity<TAny>(string sql, Dictionary<string, object> parameters)
          where TAny : new()
        {
            using (var _dbContext = GetDbContext())
            {
                var result = _dbContext.Database.SqlQueryTAny<TAny>(sql, parameters);
                return await Task.FromResult(result);
            }
        }
        public async Task<int> ExecuteSqlCommand(string sql, params object[] parameters)
        {
            var _dbContext = GetDbContext();
            return await _dbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }
        #endregion

        #region Insert


        public TEntity Insert(TEntity entity)
        {
            using (var _dbContext = GetDbContext())
            {
                var result = _dbContext.Add(entity).Entity;
                _dbContext.SaveChanges();
                return result;
            }
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            using (var _dbContext = GetDbContext())
            {
                var entityEntry = await _dbContext.AddAsync(entity);
                return await _dbContext.SaveChangesAsync() > 0;
            }
        }



      

        public void BatchInsert(IEnumerable<TEntity> entities)
        {
            using (var _dbContext = GetDbContext())
            {
                _dbContext.AddRange(entities);
                _dbContext.SaveChanges();
            }
        }

        public async Task BatchInsertAsync(IEnumerable<TEntity> entities)
        {
            using (var _dbContext = GetDbContext())
            {
                await _dbContext.AddRangeAsync(entities);
                await _dbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update

        public bool Update(TEntity entity)
        {
           
            using (var _dbContext = GetDbContext())
            {
                AttachIfNot(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                return _dbContext.SaveChanges() > 0;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(Update(entity));
        }

        public bool Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
           using(var dbContext= GetDbContext())
            {
              
               var entity = await dbContext.Set<TEntity>().Where(CreateEqualityExpressionForId(id)).FirstOrDefaultAsync();
                await updateAction(entity);
               return await dbContext.SaveChangesAsync()>0;
            }
          
        }

        public async Task<bool> UpdateAsync(TPrimaryKey[] ids, Func<TEntity, Task> updateAction)
        {
            using (var dbContext = GetDbContext())
            {
                for (var i = 0; i < ids.Length; i++)
                {
                    var entity = await dbContext.Set<TEntity>().Where(CreateEqualityExpressionForId(ids[i])).FirstOrDefaultAsync();
                    await updateAction(entity);
                 
                }
                await dbContext.SaveChangesAsync();
                return await Task.FromResult(true);
            }

        }
        public async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, Task> updateAction)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var entitys = _dbSet.Where(predicate);
                foreach (var entity in entitys)
                {
                    await updateAction(entity);

                }
                return await Task.FromResult(true);
            }
        }
        #endregion

        #region Delete
        public void Delete(TEntity entity)
        {
            using (var _dbContext = GetDbContext())
            {
                AttachIfNot(entity);
                _dbContext.Remove(entity);
            }

        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                await Task.FromResult(_dbSet.Remove(entity));
            }
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            //Could not found the entity, do nothing.
        }


        public async Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            await Task.FromResult(0);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(predicate);
                _dbSet.RemoveRange(data);
            }
          
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            await Task.FromResult(0);
        }


        public void BatchRemove(IEnumerable<TEntity> entities)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                _dbSet.RemoveRange(entities);
            }
        }

        public async Task BatchRemoveAsync(IEnumerable<TEntity> entities)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                _dbSet.RemoveRange(entities);
                await Task.FromResult(0);
            }
        }
        #endregion

        #region Aggregates
       

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(predicate);
                return data.Count();
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(predicate);
                return await data.CountAsync();
            }
        }

      


        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(predicate);
                return data.LongCount();
            }
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (var _dbContext = GetDbContext())
            {
                var _dbSet = _dbContext.Set<TEntity>();
                var data = _dbSet.Where(predicate);
                return await data.LongCountAsync();
            }
        }


        #endregion

    }
}

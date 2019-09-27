using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Core
{
    public interface IRespositoryBase<TEntity> : IRespositoryBase<TEntity, string>

       where TEntity : class
    {

    }


    public interface IRespositoryBase<TEntity, TPrimaryKey> : IDependency
        where TEntity : class
    {

      


        #region Select/Get/Query

     


        Task<IEnumerable<TEntity>> EntitiesByExpressionAsync(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TResult>> EntitiesByExpressionAsync<TResult>(Expression<Func<TEntity, bool>> expression,
           Expression<Func<TEntity, TResult>> selector) where TResult:class;
        IEnumerable<TEntity> EntitiesByExpression(Expression<Func<TEntity, bool>> expression);

        IEnumerable<TResult> EntitiesByExpression<TResult>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity,TResult>> selector) where TResult : class;

        IEnumerable<IGrouping<Tkey, TEntity>>
              GroupBy<Tkey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, Tkey>> keySelector);

        Task<IEnumerable<IGrouping<Tkey, TEntity>>>
              GroupByAsync<Tkey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, Tkey>> keySelector);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

       

     // IQueryable<TReadModel> Table<TReadModel>() where TReadModel : class;

        Task<IEnumerable<TEntity>> WherePaged<TKey>(PageData pageData,
            Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> keySelector, bool isAsc = true);

        Task<IEnumerable<TResult>> WherePaged<TResult, TKey>(PageData pageData,
            Expression<Func<TEntity, bool>> whereLambda
            ,Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, TKey>> keySelector, bool isAsc = true) where TResult:class;

        Task<IEnumerable<TEntity>> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters);

        Task<DataTable> SqlQueryDataTable(string sql, Dictionary<string, object> parameters);

        Task<List<TAny>> SqlQueryEntity<TAny>(string sql, Dictionary<string, object> parameters)
            where TAny:new ();

        Task<int> ExecuteSqlCommand(string sql, params object[] parameters);

        #endregion

        #region Insert


        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        Task<bool> InsertAsync(TEntity entity);

    

      


        void BatchInsert(IEnumerable<TEntity> entities);

        Task BatchInsertAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        bool Update(TEntity entity);

        /// <summary>
        /// Updates an existing entity. 
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="updateAction">Action that can be used to change values of the entity</param>
        /// <returns>Updated entity</returns>
        bool Update(TPrimaryKey id, Action<TEntity> updateAction);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="updateAction">Action that can be used to change values of the entity</param>
        /// <returns>Updated entity</returns>
        Task<bool> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        Task<bool> UpdateAsync(TPrimaryKey[] ids, Func<TEntity, Task> updateAction);

        Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, Task> updateAction);
        #endregion

        #region Delete

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        Task DeleteAsync(TPrimaryKey id);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);


        void BatchRemove(IEnumerable<TEntity> entities);
        Task BatchRemoveAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Aggregates

      

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

      

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion




    }
}

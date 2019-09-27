using MicroService.Common.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MicroService.Data.Common
{
    public static class PageExtensions
    {
        public static IQueryable<TSource> ToPaginated<TSource>(this IQueryable<TSource> sources
          , PageData pageData)
        {

            if (sources == null)
            {
                throw new ArgumentNullException("sources");
            }
            pageData.Total = sources.Count();
            return sources.Skip((pageData.PageIndex - 1) * pageData.PageSize).Take(pageData.PageSize);
        }

        /// <summary>
        /// 统一分页扩展方法
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <typeparam name="Tkey">字段</typeparam>
        /// <param name="source">数据集资源</param>
        /// <param name="pageData">分页信息</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public static IQueryable<T> WherePaged<T, Tkey>(this IQueryable<T> source, PageData pageData,
            Expression<Func<T, bool>> whereLambda, Expression<Func<T, Tkey>> keySelector, bool isAsc = true) where T : new()
        {

            IOrderedQueryable<T> iOrderQuery = null;
            if (isAsc)
                iOrderQuery = source.OrderBy(keySelector);
            else
                iOrderQuery = source.OrderByDescending(keySelector);

            pageData.Total = iOrderQuery.Where(whereLambda).Count();
            return
                iOrderQuery.Where(whereLambda).Skip((pageData.PageIndex - 1) * pageData.PageSize)
                 .Take(pageData.PageSize);
        }
    }
}

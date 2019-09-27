
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Validation;
using MicroService.Common.Models;
using MicroService.Entity.Product;
using MicroService.IApplication.Product.Dto;

 namespace MicroService.IApplication.Product
 {
	/// <summary>
	/// Goods -applaction实现
	/// </summary>
	public interface IGoodsAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="goodsRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(GoodsRequestDto goodsRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="goodsRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<GoodsRequestDto> goodsRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="goodsPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(GoodsPageRequestDto goodsPageRequestDto);
		
		 /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<GoodsQueryDto>> QueryAsync(Expression<Func<Goods, bool>> predicate);

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<GoodsQueryDto> QuerySingleAsync(Expression<Func<Goods, bool>> predicate);

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<GoodsQueryDto> GetGoodsById(string Id);

		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<GoodsQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="goodsRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(GoodsRequestDto goodsRequestDto);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(params string[] ids);
	}
}
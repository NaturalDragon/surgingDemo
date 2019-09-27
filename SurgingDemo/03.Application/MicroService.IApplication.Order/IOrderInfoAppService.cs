
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
using MicroService.Entity.Order;
using MicroService.IApplication.Order.Dto;

 namespace MicroService.IApplication.Order
 {
	/// <summary>
	/// OrderInfo -applaction实现
	/// </summary>
	public interface IOrderInfoAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="orderInfoRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(OrderInfoRequestDto orderInfoRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="orderInfoRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<OrderInfoRequestDto> orderInfoRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="orderInfoPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(OrderInfoPageRequestDto orderInfoPageRequestDto);
		
		 /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<OrderInfoQueryDto>> QueryAsync(Expression<Func<OrderInfo, bool>> predicate);

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<OrderInfoQueryDto> QuerySingleAsync(Expression<Func<OrderInfo, bool>> predicate);

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<OrderInfoQueryDto> GetOrderInfoById(string Id);

		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<OrderInfoQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="orderInfoRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(OrderInfoRequestDto orderInfoRequestDto);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(params string[] ids);
	}
}
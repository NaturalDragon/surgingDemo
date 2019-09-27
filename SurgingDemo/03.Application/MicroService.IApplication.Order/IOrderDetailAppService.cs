
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
	/// OrderDetail -applaction实现
	/// </summary>
	public interface IOrderDetailAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="orderDetailRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(OrderDetailRequestDto orderDetailRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="orderDetailRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<OrderDetailRequestDto> orderDetailRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="orderDetailPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(OrderDetailPageRequestDto orderDetailPageRequestDto);
		
		 /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<OrderDetailQueryDto>> QueryAsync(Expression<Func<OrderDetail, bool>> predicate);

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<OrderDetailQueryDto> QuerySingleAsync(Expression<Func<OrderDetail, bool>> predicate);

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<OrderDetailQueryDto> GetOrderDetailById(string Id);

		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<OrderDetailQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="orderDetailRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(OrderDetailRequestDto orderDetailRequestDto);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(params string[] ids);
	}
}
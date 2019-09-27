
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Common.Models;
using MicroService.Data.Validation;
using MicroService.IApplication.Order.Dto;
using Surging.Core.CPlatform.Filters.Implementation;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
 namespace MicroService.IModules.Order
 {
	/// <summary>
	/// OrderDetail -IModule接口
	/// </summary>
	[ServiceBundle("api/{Service}")]
	public interface IOrderDetailService: IServiceKey
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
	    [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Create(OrderDetailRequestDto input);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> BatchCreate(OrderDetailBatchRequestDto input);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<PageData> GetPageList(OrderDetailPageRequestDto input);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<OrderDetailQueryDto> GetForModify(EntityQueryRequest input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Modify(OrderDetailRequestDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Remove(EntityRequest input);
	}
}
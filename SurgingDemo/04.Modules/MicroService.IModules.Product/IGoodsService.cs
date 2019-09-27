
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
using MicroService.IApplication.Product.Dto;
using Surging.Core.CPlatform.Filters.Implementation;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
 namespace MicroService.IModules.Product
 {
	/// <summary>
	/// Goods -IModule接口
	/// </summary>
	[ServiceBundle("api/{Service}")]
	public interface IGoodsService: IServiceKey
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
	    [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Create(GoodsRequestDto input);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> BatchCreate(GoodsBatchRequestDto input);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<PageData> GetPageList(GoodsPageRequestDto input);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<GoodsQueryDto> GetForModify(EntityQueryRequest input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Modify(GoodsRequestDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Remove(EntityRequest input);

        /// <summary>
        /// 根据商品id集合获取商品信息
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<IList<GoodsQueryDto>> GetGoodsByIds(EntityQueryRequest entityQueryRequest);
	}
}

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
using MicroService.IApplication.Org.Dto;
    using Surging.Core.CPlatform.Filters.Implementation;
    using Surging.Core.CPlatform.Ioc;
    using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
 namespace MicroService.IModules.Org
 {
	/// <summary>
	/// Roles -IModule接口
	/// </summary>
	[ServiceBundle("api/{Service}")]
	public interface IRolesService: IServiceKey
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
	    [Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Create(RolesRequestDto input);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> BatchCreate(RolesBatchRequestDto input);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<PageData> GetPageList(RolesPageRequestDto input);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<RolesQueryDto> GetForModify(EntityQueryRequest input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Modify(RolesRequestDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        Task<JsonResponse> Remove(EntityRequest input);
	}
}
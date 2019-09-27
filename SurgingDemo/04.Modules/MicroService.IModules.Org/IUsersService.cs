
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
using Surging.Core.CPlatform.Support.Attributes;
using Surging.Core.CPlatform.Support;
using MicroService.FuseStrategy.Injection;
using Surging.Core.System.Intercept;
using Surging.Core.Caching;

namespace MicroService.IModules.Org
 {
	/// <summary>
	/// Users -IModule接口
	/// </summary>
	[ServiceBundle("api/{Service}")]
	public interface IUsersService: IServiceKey
	{
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LoginUser> Authentication(UsersRequestDto input);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection,Injection = Strategy.BaseStrategy,RequestCacheEnabled = true,InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces})]
        Task<JsonResponse> Create(UsersRequestDto input);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.BaseStrategy, RequestCacheEnabled = true, InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces })]
        Task<JsonResponse> BatchCreate(UsersBatchRequestDto input);

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorization(AuthType = AuthorizationType.JWT)]
        [HttpPost(true)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
       // [InterceptMethod(CachingMethod.Get, "GetUser_id_{0}", "GetUserName_name_{0}", Mode = CacheTargetType.Redis)]
        Task<PageData> GetPageList(UsersPageRequestDto input);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		//[Authorization(AuthType = AuthorizationType.JWT)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.MessageStrategy, RequestCacheEnabled = true)]
        [InterceptMethod(CachingMethod.Get, Key = "GetId_{0}", CacheSectionType = SectionType.ddlCache, 
            Mode = CacheTargetType.Redis, Time = 480)]
        Task<UsersQueryDto> GetForModify(EntityQueryRequest input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.BaseStrategy, RequestCacheEnabled = true, InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces })]
        Task<JsonResponse> Modify(UsersRequestDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorization(AuthType = AuthorizationType.JWT)]
        [Command(Strategy = StrategyType.Injection, Injection = Strategy.BaseStrategy, RequestCacheEnabled = true, InjectionNamespaces = new string[] { InjectionNamespace.JsonResponseNamespaces })]
        Task<JsonResponse> Remove(EntityRequest input);
	}
}
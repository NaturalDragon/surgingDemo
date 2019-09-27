
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;
using MicroService.Data.Common;
using MicroService.Data.Extensions;
using MicroService.Data.Validation;
using MicroService.Common.Models;
using MicroService.IApplication.Org;
using MicroService.IApplication.Org.Dto;
using MicroService.IModules.Org;
using Newtonsoft.Json;

 namespace MicroService.Modules.Org
 {
	/// <summary>
	/// Roles -Module实现
	/// </summary>
	[ModuleName("Roles")]
	public class RolesService: ProxyServiceBase, IRolesService
	{
	    private readonly IRolesAppService _rolesAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public RolesService(IRolesAppService rolesAppService)
        {
            _rolesAppService = rolesAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(RolesRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _rolesAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(RolesBatchRequestDto input)
        {
			foreach (var rolesRequestDto in input.RolesRequestList)
            {
                rolesRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _rolesAppService.BatchCreateAsync(input.RolesRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(RolesPageRequestDto input)
        { 
			input.InitRequest();
            return await _rolesAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RolesQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _rolesAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(RolesRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _rolesAppService.ModifyAsync(input);
            });
            return resJson;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Remove(EntityRequest input)
        {
			input.InitRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _rolesAppService.RemoveAsync(input.Ids.ToArray());
            });
            return resJson;
        }
	}
}
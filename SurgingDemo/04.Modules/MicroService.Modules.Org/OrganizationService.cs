
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
	/// Organization -Module实现
	/// </summary>
	[ModuleName("Organization")]
	public class OrganizationService: ProxyServiceBase, IOrganizationService
	{
	    private readonly IOrganizationAppService _organizationAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public OrganizationService(IOrganizationAppService organizationAppService)
        {
            _organizationAppService = organizationAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(OrganizationRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _organizationAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(OrganizationBatchRequestDto input)
        {
			foreach (var organizationRequestDto in input.OrganizationRequestList)
            {
                organizationRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _organizationAppService.BatchCreateAsync(input.OrganizationRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(OrganizationPageRequestDto input)
        { 
			input.InitRequest();
            return await _organizationAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OrganizationQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _organizationAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(OrganizationRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _organizationAppService.ModifyAsync(input);
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
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _organizationAppService.RemoveAsync(input);
            });
            return resJson;
        }
	}
}
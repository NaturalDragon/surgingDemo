
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
using Surging.Core.CPlatform.Transport.Implementation;

namespace MicroService.Modules.Org
 {
    /// <summary>
    /// Users -Module实现
    /// </summary>
    [ModuleName("Users")]
    public class UsersService : ProxyServiceBase, IUsersService
    {
        private readonly IUsersAppService _usersAppService;

        private readonly ApplicationEnginee _applicationEnginee;
        public UsersService(IUsersAppService usersAppService)
        {
            _usersAppService = usersAppService;
            _applicationEnginee = new ApplicationEnginee();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LoginUser> Authentication(UsersRequestDto input)
        {
            return await _usersAppService.Login(input);
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(UsersRequestDto input)
        {


           // input.InitCreateRequest();
            input.Id = Guid.NewGuid().ToString();
            input.RoleId = "DD1AF84F-7570-4D1E-93F6-F67330689367";
            input.CreateUserId = "00000000-0000-0000-0000-000000000000";
            input.PhoneCode = "12332121";
            input.CreateUserName = "admin";
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _usersAppService.CreateAsync(input);
            });

            return resJson;
        } 
           
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(UsersBatchRequestDto input)
        {
			foreach (var usersRequestDto in input.UsersRequestList)
            {
                usersRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _usersAppService.BatchCreateAsync(input.UsersRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(UsersPageRequestDto input)
        { 
			//input.InitRequest();
            return await _usersAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UsersQueryDto> GetForModify(EntityQueryRequest input)
        {
            var p=  RpcContext.GetContext().GetAttachment("payload");
            Console.WriteLine("来了老弟!");
           // input.InitRequest();
            return await _usersAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(UsersRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _usersAppService.ModifyAsync(input);
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
               await _usersAppService.RemoveAsync(input.Ids.ToArray());
            });
            return resJson;
        }
	}
}
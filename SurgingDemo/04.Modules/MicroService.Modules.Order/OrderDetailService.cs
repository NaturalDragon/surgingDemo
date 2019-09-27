
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
using MicroService.IApplication.Order;
using MicroService.IApplication.Order.Dto;
using MicroService.IModules.Order;
using Newtonsoft.Json;

 namespace MicroService.Modules.Order
 {
	/// <summary>
	/// OrderDetail -Module实现
	/// </summary>
	[ModuleName("OrderDetail")]
	public class OrderDetailService: ProxyServiceBase, IOrderDetailService
	{
	    private readonly IOrderDetailAppService _orderDetailAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public OrderDetailService(IOrderDetailAppService orderDetailAppService)
        {
            _orderDetailAppService = orderDetailAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(OrderDetailRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _orderDetailAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(OrderDetailBatchRequestDto input)
        {
			foreach (var orderDetailRequestDto in input.OrderDetailRequestList)
            {
                orderDetailRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _orderDetailAppService.BatchCreateAsync(input.OrderDetailRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(OrderDetailPageRequestDto input)
        { 
			input.InitRequest();
            return await _orderDetailAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OrderDetailQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _orderDetailAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(OrderDetailRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _orderDetailAppService.ModifyAsync(input);
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
               await _orderDetailAppService.RemoveAsync(input.Ids.ToArray());
            });
            return resJson;
        }
	}
}

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
	/// OrderInfo -Module实现
	/// </summary>
	[ModuleName("OrderInfo")]
	public class OrderInfoService: ProxyServiceBase, IOrderInfoService
	{
	    private readonly IOrderInfoAppService _orderInfoAppService;
        private readonly IOrderDetailAppService _orderDetailAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public OrderInfoService(IOrderInfoAppService orderInfoAppService, IOrderDetailAppService orderDetailAppService)
        {
            _orderInfoAppService = orderInfoAppService;
            _orderDetailAppService = orderDetailAppService;
            _applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(OrderInfoRequestDto input)
        {
            input.InitCreateRequest();
            input.UserId = input.CreateUserId;
            List<GoodsRequestDto> goodsQuerys = await RpcService.GetGoodsAsync(input);
            List<OrderDetailRequestDto> orderDetailRequestDtos = ToDetails(input, goodsQuerys);
            input.OrderNumber = DateTime.Now.ToString();
            input.TotalMoney = orderDetailRequestDtos.Select(d => d.Money).Sum();
            input.ExpireTime = DateTime.Now.AddDays(14);
            var resJson = await new ApplicationEnginee().TryTransactionAsync(async () =>
            {
                await _orderInfoAppService.CreateAsync(input);
                await _orderDetailAppService.BatchCreateAsync(orderDetailRequestDtos);
            });

            return resJson;
        }

        private static List<OrderDetailRequestDto> ToDetails(OrderInfoRequestDto input, List<GoodsRequestDto> goodsQuerys)
        {
            List<OrderDetailRequestDto> orderDetailRequestDtos = new List<OrderDetailRequestDto>();
            input.Id = Guid.NewGuid().ToString();
            foreach (var item in goodsQuerys)
            {
                var good = input.GoodsRequestDtos.Where(g => g.Id == item.Id).FirstOrDefault();
                var orderDetailRequest = new OrderDetailRequestDto()
                {
                    Id = Guid.NewGuid().ToString(),
                    GoodsId = item.Id,
                    OrderId = input.Id,
                    Count = good.Count,
                    Price = item.Price,
                    Money = good.Count * item.Price
                };
                orderDetailRequest.InitCreateRequest(input.Payload);
                orderDetailRequestDtos.Add(orderDetailRequest);
            }
            return orderDetailRequestDtos;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(OrderInfoBatchRequestDto input)
        {
			foreach (var orderInfoRequestDto in input.OrderInfoRequestList)
            {
                orderInfoRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _orderInfoAppService.BatchCreateAsync(input.OrderInfoRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(OrderInfoPageRequestDto input)
        { 
			input.InitRequest();
            return await _orderInfoAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OrderInfoQueryDto> GetForModify(EntityQueryRequest input)
        {
          //  input.InitRequest();
            var orderInfo = await _orderInfoAppService.GetForModifyAsync(input);
            var userInfo = await RpcService.GetUserByIdAsync(orderInfo.UserId,input.Payload);
            orderInfo.UserName = userInfo.Name;
            return orderInfo;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(OrderInfoRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _orderInfoAppService.ModifyAsync(input);
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
               await _orderInfoAppService.RemoveAsync(input.Ids.ToArray());
            });
            return resJson;
        }
	}
}
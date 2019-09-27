
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
using MicroService.IApplication.Product;
using MicroService.IApplication.Product.Dto;
using MicroService.IModules.Product;
using Newtonsoft.Json;

 namespace MicroService.Modules.Product
 {
	/// <summary>
	/// Goods -Module实现
	/// </summary>
	[ModuleName("Goods")]
	public class GoodsService: ProxyServiceBase, IGoodsService
	{
	    private readonly IGoodsAppService _goodsAppService;
		private readonly ApplicationEnginee _applicationEnginee;
        public GoodsService(IGoodsAppService goodsAppService)
        {
            _goodsAppService = goodsAppService;
			_applicationEnginee = new ApplicationEnginee();
        }
		/// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Create(GoodsRequestDto input)
        {
			input.InitCreateRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _goodsAppService.CreateAsync(input);
            });
            return resJson;
        }
         /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> BatchCreate(GoodsBatchRequestDto input)
        {
			foreach (var goodsRequestDto in input.GoodsRequestList)
            {
                goodsRequestDto.InitCreateRequest(input.Payload);
            }
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
               await _goodsAppService.BatchCreateAsync(input.GoodsRequestList);
            });
            return resJson;
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData> GetPageList(GoodsPageRequestDto input)
        { 
			input.InitRequest();
            return await _goodsAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GoodsQueryDto> GetForModify(EntityQueryRequest input)
        {
			input.InitRequest();
            return await _goodsAppService.GetForModifyAsync(input);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResponse> Modify(GoodsRequestDto input)
        {
			input.InitModifyRequest();
            var resJson = await _applicationEnginee.TryTransactionAsync(async () =>
            {
                await _goodsAppService.ModifyAsync(input);
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
               await _goodsAppService.RemoveAsync(input.Ids.ToArray());
            });
            return resJson;
        }
        /// <summary>
        /// 根据商品id集合获取商品信息
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<IList<GoodsQueryDto>> GetGoodsByIds(EntityQueryRequest entityQueryRequest)
        {
            return await _goodsAppService.QueryAsync(g => entityQueryRequest.Ids.Contains(g.Id));
        }
    }
}
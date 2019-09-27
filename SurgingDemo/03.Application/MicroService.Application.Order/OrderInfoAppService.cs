
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Data.Ext;
using MicroService.IApplication.Order;
using MicroService.IRespository.Order;
using MicroService.Entity.Order;
using MicroService.IApplication.Order.Dto;
using MicroService.Core.Data;
using MicroService.Data.Validation;
using MicroService.Data.Common;
using MicroService.Common.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicroService.Application.Order.ValidatorsFilters;
 namespace MicroService.Application.Order
 {
	/// <summary>
	/// OrderInfo -applaction实现
	/// </summary>
	public class OrderInfoAppService:IOrderInfoAppService
	{
     
        private readonly IOrderInfoRespository _orderInfoRespository;
        private readonly IMapper _mapper;
        public OrderInfoAppService(IOrderInfoRespository orderInfoRespository,
          IMapper mapper)
        {
            _orderInfoRespository = orderInfoRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="orderInfoRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(OrderInfoRequestDto orderInfoRequestDto)
        {
         
            var orderInfo = _mapper.Map<OrderInfoRequestDto, OrderInfo>(orderInfoRequestDto);
            await OrderInfoValidatorsFilter.DoValidationAsync(_orderInfoRespository,orderInfo, ValidatorTypeConstants.Create);
            return await _orderInfoRespository.InsertAsync(orderInfo);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="orderInfoRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<OrderInfoRequestDto> orderInfoRequestDtos)
        {
            var entities = orderInfoRequestDtos.MapToList<OrderInfoRequestDto, OrderInfo>();
            await OrderInfoValidatorsFilter.DoValidationAsync(_orderInfoRespository,entities, ValidatorTypeConstants.Create);
            await _orderInfoRespository.BatchInsertAsync(entities);
            return true;
         
        }

      
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="orderInfoPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( OrderInfoPageRequestDto orderInfoPageRequestDto)
        {
            var pageData = new PageData(orderInfoPageRequestDto.PageIndex, orderInfoPageRequestDto.PageSize);
            var list = await _orderInfoRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<OrderInfo, OrderInfoQueryDto>().ToList();
            return pageData;

        }

		/// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<OrderInfoQueryDto>> QueryAsync(Expression<Func<OrderInfo, bool>> predicate)
        {
            var list =await  _orderInfoRespository.EntitiesByExpressionAsync(predicate); 
            return list.MapToList<OrderInfo, OrderInfoQueryDto>().ToList(); ;

        }
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<OrderInfoQueryDto> QuerySingleAsync(Expression<Func<OrderInfo, bool>> predicate)
        {
            var entity = await _orderInfoRespository.FirstOrDefaultAsync(predicate);
			if (entity != null)
            {
                return entity.MapEntity<OrderInfo, OrderInfoQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OrderInfoQueryDto> GetOrderInfoById(string Id)
        {
            var entity = await _orderInfoRespository.FirstOrDefaultAsync(e => e.Id == Id);
            if (entity != null)
            {
                return entity.MapEntity<OrderInfo, OrderInfoQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<OrderInfoQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _orderInfoRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<OrderInfo, OrderInfoQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="orderInfoRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(OrderInfoRequestDto orderInfoRequestDto)
        {
            var orderInfo = await _orderInfoRespository.FirstOrDefaultAsync(e => e.Id == orderInfoRequestDto.Id);
            var entity = orderInfoRequestDto.MapToModifyEntity<OrderInfoRequestDto, OrderInfo>(orderInfo);
            await OrderInfoValidatorsFilter.DoValidationAsync(_orderInfoRespository, entity, ValidatorTypeConstants.Modify);
            return await _orderInfoRespository.UpdateAsync(entity); 
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(params string[] ids)
        {
          return  await _orderInfoRespository.UpdateAsync(ids,  async (e)=> {
               await Task.Run(() =>
              {
                  e.IsDelete = true;
              });
           });
         
        }
	}
}
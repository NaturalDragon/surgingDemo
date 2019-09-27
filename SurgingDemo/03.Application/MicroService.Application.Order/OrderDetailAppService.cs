
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
	/// OrderDetail -applaction实现
	/// </summary>
	public class OrderDetailAppService:IOrderDetailAppService
	{
     
        private readonly IOrderDetailRespository _orderDetailRespository;
        private readonly IMapper _mapper;
        public OrderDetailAppService(IOrderDetailRespository orderDetailRespository,
          IMapper mapper)
        {
            _orderDetailRespository = orderDetailRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="orderDetailRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(OrderDetailRequestDto orderDetailRequestDto)
        {
         
            var orderDetail = _mapper.Map<OrderDetailRequestDto, OrderDetail>(orderDetailRequestDto);
            await OrderDetailValidatorsFilter.DoValidationAsync(_orderDetailRespository,orderDetail, ValidatorTypeConstants.Create);
            return await _orderDetailRespository.InsertAsync(orderDetail);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="orderDetailRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<OrderDetailRequestDto> orderDetailRequestDtos)
        {
            var entities = orderDetailRequestDtos.MapToList<OrderDetailRequestDto, OrderDetail>();
            await OrderDetailValidatorsFilter.DoValidationAsync(_orderDetailRespository,entities, ValidatorTypeConstants.Create);
            await _orderDetailRespository.BatchInsertAsync(entities);
            return true;
         
        }

      
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="orderDetailPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( OrderDetailPageRequestDto orderDetailPageRequestDto)
        {
            var pageData = new PageData(orderDetailPageRequestDto.PageIndex, orderDetailPageRequestDto.PageSize);
            var list = await _orderDetailRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<OrderDetail, OrderDetailQueryDto>().ToList();
            return pageData;

        }

		/// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<OrderDetailQueryDto>> QueryAsync(Expression<Func<OrderDetail, bool>> predicate)
        {
            var list =await  _orderDetailRespository.EntitiesByExpressionAsync(predicate); 
            return list.MapToList<OrderDetail, OrderDetailQueryDto>().ToList(); ;

        }
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<OrderDetailQueryDto> QuerySingleAsync(Expression<Func<OrderDetail, bool>> predicate)
        {
            var entity = await _orderDetailRespository.FirstOrDefaultAsync(predicate);
			if (entity != null)
            {
                return entity.MapEntity<OrderDetail, OrderDetailQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<OrderDetailQueryDto> GetOrderDetailById(string Id)
        {
            var entity = await _orderDetailRespository.FirstOrDefaultAsync(e => e.Id == Id);
            if (entity != null)
            {
                return entity.MapEntity<OrderDetail, OrderDetailQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<OrderDetailQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _orderDetailRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<OrderDetail, OrderDetailQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="orderDetailRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(OrderDetailRequestDto orderDetailRequestDto)
        {
            var orderDetail = await _orderDetailRespository.FirstOrDefaultAsync(e => e.Id == orderDetailRequestDto.Id);
            var entity = orderDetailRequestDto.MapToModifyEntity<OrderDetailRequestDto, OrderDetail>(orderDetail);
            await OrderDetailValidatorsFilter.DoValidationAsync(_orderDetailRespository, entity, ValidatorTypeConstants.Modify);
            return await _orderDetailRespository.UpdateAsync(entity); 
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(params string[] ids)
        {
          return  await _orderDetailRespository.UpdateAsync(ids,  async (e)=> {
               await Task.Run(() =>
              {
                  e.IsDelete = true;
              });
           });
         
        }
	}
}

using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Data.Ext;
using MicroService.IApplication.Product;
using MicroService.IRespository.Product;
using MicroService.Entity.Product;
using MicroService.IApplication.Product.Dto;
using MicroService.Core.Data;
using MicroService.Data.Validation;
using MicroService.Data.Common;
using MicroService.Common.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicroService.Application.Product.ValidatorsFilters;
 namespace MicroService.Application.Product
 {
	/// <summary>
	/// Goods -applaction实现
	/// </summary>
	public class GoodsAppService:IGoodsAppService
	{
     
        private readonly IGoodsRespository _goodsRespository;
        private readonly IMapper _mapper;
        public GoodsAppService(IGoodsRespository goodsRespository,
          IMapper mapper)
        {
            _goodsRespository = goodsRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="goodsRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(GoodsRequestDto goodsRequestDto)
        {
         
            var goods = _mapper.Map<GoodsRequestDto, Goods>(goodsRequestDto);
            await GoodsValidatorsFilter.DoValidationAsync(_goodsRespository,goods, ValidatorTypeConstants.Create);
            return await _goodsRespository.InsertAsync(goods);  
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="goodsRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<GoodsRequestDto> goodsRequestDtos)
        {
            var entities = goodsRequestDtos.MapToList<GoodsRequestDto, Goods>();
            await GoodsValidatorsFilter.DoValidationAsync(_goodsRespository,entities, ValidatorTypeConstants.Create);
            await _goodsRespository.BatchInsertAsync(entities);
            return true;
         
        }

      
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="goodsPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( GoodsPageRequestDto goodsPageRequestDto)
        {
            var pageData = new PageData(goodsPageRequestDto.PageIndex, goodsPageRequestDto.PageSize);
            var list = await _goodsRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<Goods, GoodsQueryDto>().ToList();
            return pageData;

        }

		/// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<GoodsQueryDto>> QueryAsync(Expression<Func<Goods, bool>> predicate)
        {
            var list =await  _goodsRespository.EntitiesByExpressionAsync(predicate); 
            return list.MapToList<Goods, GoodsQueryDto>().ToList(); ;

        }
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<GoodsQueryDto> QuerySingleAsync(Expression<Func<Goods, bool>> predicate)
        {
            var entity = await _goodsRespository.FirstOrDefaultAsync(predicate);
			if (entity != null)
            {
                return entity.MapEntity<Goods, GoodsQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<GoodsQueryDto> GetGoodsById(string Id)
        {
            var entity = await _goodsRespository.FirstOrDefaultAsync(e => e.Id == Id);
            if (entity != null)
            {
                return entity.MapEntity<Goods, GoodsQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<GoodsQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _goodsRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<Goods, GoodsQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="goodsRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(GoodsRequestDto goodsRequestDto)
        {
            var goods=await  _goodsRespository.FirstOrDefaultAsync(e => e.Id == goodsRequestDto.Id);
            var entity= goodsRequestDto.MapToModifyEntity<GoodsRequestDto, Goods>(goods);
            await GoodsValidatorsFilter.DoValidationAsync(_goodsRespository, entity, ValidatorTypeConstants.Modify);
            return await _goodsRespository.UpdateAsync(entity); 
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(params string[] ids)
        {
          return  await _goodsRespository.UpdateAsync(ids,  async (e)=> {
               await Task.Run(() =>
              {
                  e.IsDelete = true;
              });
           });
         
        }
	}
}
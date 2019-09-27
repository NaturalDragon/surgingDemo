
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Data.Ext;
using MicroService.IApplication.Org;
using MicroService.IRespository.Org;
using MicroService.Entity.Org;
using MicroService.IApplication.Org.Dto;
using MicroService.Core.Data;
using MicroService.Data.Validation;
using MicroService.Data.Common;
using MicroService.Common.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicroService.Application.Org.ValidatorsFilters;
 namespace MicroService.Application.Org
 {
	/// <summary>
	/// Roles -applaction实现
	/// </summary>
	public class RolesAppService:IRolesAppService
	{
     
        private readonly IRolesRespository _rolesRespository;
        private readonly IMapper _mapper;
        public RolesAppService(IRolesRespository rolesRespository,
          IMapper mapper)
        {
            _rolesRespository = rolesRespository;
            _mapper = mapper;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="rolesRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(RolesRequestDto rolesRequestDto)
        {

            var roles = _mapper.Map<RolesRequestDto, Roles>(rolesRequestDto);
            await RolesValidatorsFilter.DoValidationAsync(_rolesRespository, roles, ValidatorTypeConstants.Create);
            return await _rolesRespository.InsertAsync(roles);

        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="rolesRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<RolesRequestDto> rolesRequestDtos)
        {
            var entities = rolesRequestDtos.MapToList<RolesRequestDto, Roles>();
            await RolesValidatorsFilter.DoValidationAsync(_rolesRespository,entities, ValidatorTypeConstants.Create);
            await _rolesRespository.BatchInsertAsync(entities);
            return true;
         
        }

      
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="rolesPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( RolesPageRequestDto rolesPageRequestDto)
        {
            var pageData = new PageData(rolesPageRequestDto.PageIndex, rolesPageRequestDto.PageSize);
            var list = await _rolesRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<Roles, RolesQueryDto>().ToList();
            return pageData;

        }

		/// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<RolesQueryDto>> QueryAsync(Expression<Func<Roles, bool>> predicate)
        {
            var list =await  _rolesRespository.EntitiesByExpressionAsync(predicate); 
            return list.MapToList<Roles, RolesQueryDto>().ToList(); ;

        }
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<RolesQueryDto> QuerySingleAsync(Expression<Func<Roles, bool>> predicate)
        {
            var entity = await _rolesRespository.FirstOrDefaultAsync(predicate);
			if (entity != null)
            {
                return entity.MapEntity<Roles, RolesQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RolesQueryDto> GetRolesById(string Id)
        {
            var entity = await _rolesRespository.FirstOrDefaultAsync(e => e.Id == Id);
            if (entity != null)
            {
                return entity.MapEntity<Roles, RolesQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<RolesQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _rolesRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<Roles, RolesQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="rolesRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(RolesRequestDto rolesRequestDto)
        {
            var roles = await _rolesRespository.FirstOrDefaultAsync(e => e.Id == rolesRequestDto.Id);
            var entity = rolesRequestDto.MapToModifyEntity<RolesRequestDto, Roles>(roles);
            await RolesValidatorsFilter.DoValidationAsync(_rolesRespository, entity, ValidatorTypeConstants.Modify);
            return await _rolesRespository.UpdateAsync(entity);
          
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(params string[] ids)
        {
          return await _rolesRespository.UpdateAsync(ids,  async (e)=> {
               await Task.Run(() =>
              {
                  e.IsDelete = true;
              });
           });
        }
	}
}
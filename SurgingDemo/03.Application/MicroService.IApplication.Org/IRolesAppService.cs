
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Validation;
using MicroService.Common.Models;
using MicroService.Entity.Org;
using MicroService.IApplication.Org.Dto;

 namespace MicroService.IApplication.Org
 {
	/// <summary>
	/// Roles -applaction实现
	/// </summary>
	public interface IRolesAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="rolesRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(RolesRequestDto rolesRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="rolesRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<RolesRequestDto> rolesRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="rolesPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(RolesPageRequestDto rolesPageRequestDto);
		
		 /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<RolesQueryDto>> QueryAsync(Expression<Func<Roles, bool>> predicate);

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<RolesQueryDto> QuerySingleAsync(Expression<Func<Roles, bool>> predicate);

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<RolesQueryDto> GetRolesById(string Id);

		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<RolesQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="rolesRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(RolesRequestDto rolesRequestDto);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(params string[] ids);
	}
}
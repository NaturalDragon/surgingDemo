
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
	/// Users -applaction实现
	/// </summary>
	public interface IUsersAppService: IDependency
	{
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="usersRequestDto"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(UsersRequestDto usersRequestDto);
		/// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="usersRequestDtos"></param>
        /// <returns></returns>
        Task<bool> BatchCreateAsync(IList<UsersRequestDto> usersRequestDtos);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="usersPageRequestDto"></param>
        /// <returns></returns>
        Task<PageData> GetPageListAsync(UsersPageRequestDto usersPageRequestDto);
		
		 /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<UsersQueryDto>> QueryAsync(Expression<Func<Users, bool>> predicate);

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<UsersQueryDto> QuerySingleAsync(Expression<Func<Users, bool>> predicate);

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<UsersQueryDto> GetUsersById(string Id);

		/// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        Task<UsersQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest);
		/// <summary>
        /// 修改
        /// </summary>
        /// <param name="usersRequestDto"></param>
        /// <returns></returns>
        Task<bool> ModifyAsync(UsersRequestDto usersRequestDto);
		/// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(params string[] ids);

        Task<LoginUser> Login(UsersRequestDto usersRequestDto);
    }
}
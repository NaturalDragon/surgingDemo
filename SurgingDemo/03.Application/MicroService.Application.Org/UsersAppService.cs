
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
	/// Users -applaction实现
	/// </summary>
	public class UsersAppService:IUsersAppService
	{
     
        private readonly IUsersRespository _usersRespository;
        private readonly IMapper _mapper;
        public UsersAppService(IUsersRespository usersRespository,
          IMapper mapper)
        {
            _usersRespository = usersRespository;
            _mapper = mapper;
        }
    
        /// <summary>
          /// 新增
          /// </summary>
          /// <param name="usersRequestDto"></param>
          /// <returns></returns>
        public async Task<bool> CreateAsync(UsersRequestDto usersRequestDto)
        {
         
            var users = _mapper.Map<UsersRequestDto, Users>(usersRequestDto);
            await UsersValidatorsFilter.DoValidationAsync(_usersRespository,users, ValidatorTypeConstants.Create);
            return await _usersRespository.InsertAsync(users);
            
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="usersRequestDtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchCreateAsync(IList<UsersRequestDto> usersRequestDtos)
        {
            var entities = usersRequestDtos.MapToList<UsersRequestDto, Users>();
            await UsersValidatorsFilter.DoValidationAsync(_usersRespository,entities, ValidatorTypeConstants.Create);
            await _usersRespository.BatchInsertAsync(entities);
            return true;
        }

      
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="usersPageRequestDto"></param>
        /// <returns></returns>

        public async Task<PageData> GetPageListAsync( UsersPageRequestDto usersPageRequestDto)
        {
            var pageData = new PageData(usersPageRequestDto.PageIndex, usersPageRequestDto.PageSize);
            var list = await _usersRespository.WherePaged(pageData, e => e.IsDelete == false,
                e => e.CreateDate, false);
			 pageData.Data = list.MapToList<Users, UsersQueryDto>().ToList();
            return pageData;

        }

		/// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<UsersQueryDto>> QueryAsync(Expression<Func<Users, bool>> predicate)
        {
            var list =await  _usersRespository.EntitiesByExpressionAsync(predicate); 
            return list.MapToList<Users, UsersQueryDto>().ToList(); ;

        }
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<UsersQueryDto> QuerySingleAsync(Expression<Func<Users, bool>> predicate)
        {
            var entity = await _usersRespository.FirstOrDefaultAsync(predicate);
			if (entity != null)
            {
                return entity.MapEntity<Users, UsersQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<UsersQueryDto> GetUsersById(string Id)
        {
            var entity = await _usersRespository.FirstOrDefaultAsync(e => e.Id == Id);
            if (entity != null)
            {
                return entity.MapEntity<Users, UsersQueryDto>();
            }
            return null;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="entityQueryRequest"></param>
        /// <returns></returns>
        public async Task<UsersQueryDto> GetForModifyAsync(EntityQueryRequest entityQueryRequest)
        {
            var entity = await _usersRespository.FirstOrDefaultAsync(e => e.Id == entityQueryRequest.Id);
            if (entity != null)
            {
                return entity.MapEntity<Users, UsersQueryDto>();
            }
            return null;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="usersRequestDto"></param>
        /// <returns></returns>
        public async Task<bool> ModifyAsync(UsersRequestDto usersRequestDto)
        {
            var users = await _usersRespository.FirstOrDefaultAsync(e => e.Id == usersRequestDto.Id);
            var entity = usersRequestDto.MapToModifyEntity<UsersRequestDto, Users>(users);
            await UsersValidatorsFilter.DoValidationAsync(_usersRespository, entity, ValidatorTypeConstants.Modify);
            return await _usersRespository.UpdateAsync(entity);
          
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(params string[] ids)
        {
           return await _usersRespository.UpdateAsync(ids,  async (e)=> {
               await Task.Run(() =>
              {
                  e.IsDelete = true;
              });
           });
         
        }

        public async Task<LoginUser> Login(UsersRequestDto  usersRequestDto)
        {
            Dictionary<string, object> parames = new Dictionary<string, object>();
            parames.Add("Name", usersRequestDto.Name);
            parames.Add("Password", usersRequestDto.Password);
            var users = await _usersRespository.SqlQueryEntity<UsersQueryDto>(@"select u.Id,u.RoleId,u.`Password`, u.`Name` as `Name`,u.PhoneCode,r.`Name` as `RoleName` from Users u LEFT JOIN  Roles r  on u.RoleId=r.Id
where u.`Name`= @Name and u.`Password`= @Password
", parames);
            if (users.Count()<=0)
            {
                var _loginUser = new LoginUser() { IsSucceed = false, Message = "用户名或密码错误!" };
                return await Task.FromResult<LoginUser>(_loginUser);
            }
            var user = users[0];
            return await Task.FromResult(new LoginUser()
            {
                IsSucceed = true,
                Id = user.Id,
                UserId = user.Id,
                Name = user.Name,
                RoleId = user.RoleId,
                PhoneCode = user.PhoneCode
            });

        }
    }
}
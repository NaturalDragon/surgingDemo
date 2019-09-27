using MicroService.Common.Models;
using MicroService.Core;
using MicroService.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Extensions
{
   public static class LoginPayloadExtensions
    {


        public static void InitCreateRequest(this BaseDto actionRequest)
        {
            LoginUser loginUser = GetLoginUser(actionRequest.Payload);
            loginUser.RoleType = loginUser.RoleType;
            actionRequest.CreateUserId = loginUser.UserId;
            actionRequest.CreateUserName = loginUser.Name;
            actionRequest.Token = loginUser.Token;
        }


        public static void InitCreateRequest(this BaseDto actionRequest,string payload)
        {
            LoginUser loginUser = GetLoginUser(payload);
            actionRequest.CreateUserId = loginUser.UserId;
            actionRequest.CreateUserName = loginUser.Name;
            actionRequest.Token = loginUser.Token;
        }

        public static void InitRequest(this LoginUser actionRequest)
        {
            LoginUser loginUser = GetLoginUser(actionRequest.Payload);
            actionRequest.UserId = loginUser.UserId;
            actionRequest.Name = loginUser.Name;
            actionRequest.RoleType = loginUser.RoleType;
            actionRequest.Token = loginUser.Token;
        }

        public static void InitModifyRequest(this BaseDto baseDto)
        {
            LoginUser loginUser = GetLoginUser(baseDto.Payload);
            baseDto.ModifyUserId = loginUser.UserId;
            baseDto.ModifyUserName = loginUser.Name;
            baseDto.ModifyDate = DateTime.Now;
            baseDto.Token = loginUser.Token;
        }
        public static  void ToLoginUser(this LoginUser loginUser)
        {
            LoginUser login = GetLoginUser(loginUser.Payload);
            loginUser.RoleType = login.RoleType;
            if (!string.IsNullOrEmpty(login.OrgId)) {
                loginUser.OrgId = login.OrgId;
            }
            loginUser.UserId = login.UserId;
            loginUser.RoleId = login.RoleId;
            loginUser.Name = login.Name;

        }

        private static LoginUser GetLoginUser(string payLoad)
        {
            var token = payLoad.Replace("\\", "").Trim('"');
            var login = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginUser>(token);
            return login;
        }
    }
}

using MicroService.Common.Models;
using MicroService.Data.Common;
using MicroService.Data.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Core.Web.Extensions
{
    public static class LoginUserUltility
    {
      
        /// <summary>
        /// 创建LoginUser.
        /// </summary>
        /// <param name="loginUser">The login user.</param>
        /// <returns>LoginUser.</returns>
        public static LoginUser Create(LoginUser loginUser)
        {
            return new LoginUser
            {
                UserId = loginUser.UserId,
                OrgId = loginUser.OrgId,
                Name = loginUser.Name,
            };
        }

        /// <summary>
        /// 转换Token
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string ToToken(LoginUser loginUser)
        {
            var tokenTemplate = JsonConvert.SerializeObject(loginUser);

            var token = AESCryption.EncryptText(tokenTemplate, AESCryption.Salt);

            return token;
        }

        /// <summary>
        /// 转换LoginUser
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static LoginUser ParseLoginUser(string token)
        {
            var jsonTemplate = AESCryption.DecryptText(token, AESCryption.Salt);
            var loginUser = JsonConvert.DeserializeObject<LoginUser>(jsonTemplate);

            loginUser.Token = token;

            return loginUser;
        }
    }
}

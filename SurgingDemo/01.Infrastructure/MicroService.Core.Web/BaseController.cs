using MicroService.Common.Models;
using MicroService.Core.Web.Extensions;
using MicroService.Core.Web.Filter;
using MicroService.Data.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MicroService.Core.Web
{
    [EnableCors("AllowSameDomain")]
    public  class BaseController: ControllerBase
    {
        private LoginUser _loginUser;
        private bool _initLoginUser;
        /// <summary>
        /// 根据Token映射当前登录用户信息
        /// </summary>
        protected LoginUser loginUser
        {
            get
            {
                if (_initLoginUser)
                    return _loginUser;

                var value = new StringValues();

                if (Request == null)
                {
                    _loginUser = new LoginUser();
                    _initLoginUser = true;
                }
                else
                {
                    if (Request.Headers.TryGetValue(ApiActionAttribute.LOGINTOKEN, out value))
                    {
                        //临时创建loginUser
                        //_loginUser = new LoginUser
                        //{
                        //    Name = "测试",
                        //    CompanyId = string.Parse("8cb2bc43-1a56-48ed-b9c9-bdc7d791dcc5"),
                        //    UserId = string.Parse("9b454e84-70cb-40ba-8e85-64838bf8fa37"),
                        //};

                        _loginUser = GetToken(value[0]);
                        _initLoginUser = true;
                    }
                }

                return _loginUser;
            }
        }

        private LoginUser GetToken(string token)
        {
            var loginUser = new LoginUser();

            if (token == null) return loginUser;

            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }


            loginUser = LoginUserUltility.ParseLoginUser(token);

            return loginUser;
        }

        protected string loginToken
        {
            set
            {
                loginUser.Token = value;

                var loginUserString = JsonConvert.SerializeObject(loginUser);

                if (Request != null)
                {
                    Request.Headers.Add("TokenLoginUser", loginUserString);
                }
            }
        }


        protected virtual string ApiSiteIp { get; }

        public readonly static string LOGINTOKEN = HttpRequestHeader.Authorization.ToString();
    }
}

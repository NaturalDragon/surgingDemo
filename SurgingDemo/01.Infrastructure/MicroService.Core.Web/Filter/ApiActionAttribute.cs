using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MicroService.Data.Validation;
using MicroService.Data.Enums;
using MicroService.Core.Web.Extensions;

namespace MicroService.Core.Web.Filter
{
    public class ApiActionAttribute:ActionFilterAttribute
    {
        public const string LOGINUSER = "ApiActionAttribute.LoginUser";
        public readonly static string LOGINTOKEN = HttpRequestHeader.Authorization.ToString();

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (await CheckAuthToken(context))
            {
                await base.OnActionExecutionAsync(context, next);
            }
        }

        //public override void OnActionExecuting(ActionExecutingContext actionContext)
        //{
        //    if (CheckAuthToken(actionContext))
        //    {
        //        base.OnActionExecuting(actionContext);
        //    }
        //}

        private string GetAuthToken(IEnumerable<string> headerValues)
        {
            string authorization = headerValues.First();
            string token = null;

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }

            return token;
        }

        private async Task<bool> CheckAuthToken(ActionExecutingContext actionContext)
        {
            var controller = (ControllerBase)actionContext.Controller;
            //验证是否跳过登录
            if (DoseStickAttr<NoAuthTokenAttribute>(controller))
            {
                return true;
            }

            IEnumerable<string> headerValues = null;

            if (!actionContext.HttpContext.Request.Headers.ContainsKey(LOGINTOKEN))
            {
                await ToUnauthorizedResponse(actionContext);
                return false;
            }

            headerValues = actionContext.HttpContext.Request.Headers[LOGINTOKEN];

            string token = GetAuthToken(headerValues);

            if (!String.IsNullOrEmpty(token))
            {
                // 验证Token
                if (!ValidateToken(token))
                {
                    await ToUnauthorizedResponse(actionContext);
                    return false;
                }
                var ControllerName = controller.ControllerContext.ActionDescriptor.ControllerName;
                var ActionName = controller.ControllerContext.ActionDescriptor.ActionName;
                // 如果验证通过, 将LoginUser放入请求
                //actionContext.HttpContext.Request.Headers.Add(new KeyValuePair<string, StringValues>(LOGINUSER, new StringValues("Token")));
            }
            else
            {
                await ToUnauthorizedResponse(actionContext);
                return false;
            }

            return true;
        }
        /// <summary>
        ///  判断指定的方法或类上是否有指定的标记
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <returns>贴了指定标记返回true</returns>
        bool DoseStickAttr<T>(ControllerBase controller) where T : class
        {
            var actionAttributes = controller.ControllerContext.ActionDescriptor.MethodInfo.CustomAttributes;
            var controllerAttributes = controller.ControllerContext.ActionDescriptor.ControllerTypeInfo.CustomAttributes;
            var exitisAction = actionAttributes.Any(a => a.AttributeType.Equals(typeof(T)));
            var exitisController = controllerAttributes.Any(a => a.AttributeType.Equals(typeof(T)));
            return exitisAction || exitisController;
        }
        private async Task ToUnauthorizedResponse(ActionExecutingContext actionContext)
        {
            var jsonResponse = new JsonResponse
            {
                ErrorType = ErrorType.TokenExpire
            };

            // TODO 最终确定一个可以终止Webapi继续执行Action的方案
            //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            await HandleExceptionAsync(actionContext.HttpContext, (int)HttpStatusCode.Unauthorized, "Unauthorized access");
        }

        private static async Task HandleExceptionAsync(Microsoft.AspNetCore.Http.HttpContext context, int statusCode, string msg)
        {
            var data = new { code = statusCode.ToString(), is_success = false, msg = msg };
            var result = JsonConvert.SerializeObject(new { data = data });
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(result);
        }

        /// <summary>
        /// 验证token.
        /// </summary>
        /// <param name="token"></param>
        protected bool ValidateToken(string token)
        {
            //if (token == null) return false;

            //return true;

            var loginUser = LoginUserUltility.ParseLoginUser(token);

            if (loginUser.ExpireDateTime < DateTime.Now)
            {
                return true;
            }

            //第一步先验证用户token
            //第二步再次请求IdentityServer获取用户信息

            //var tokenTemplate = AESCryption.DecryptText(token,AESCryption.Salt);// RsaHelper.RsaDecrypt(token);

            //if (tokenTemplate.IsNotNullOrEmpty() && tokenTemplate.Contains("|"))
            //{
            //    var tokenArray = tokenTemplate.Split('|');

            //    if (DateTime.Now < DateTime.Parse(tokenArray[1]).AddSeconds(int.Parse(tokenArray[2])))
            //    {
            //        return true;
            //    }
            //}

            return false;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Surging.Core.ApiGateWay;
using Surging.Core.ApiGateWay.OAuth;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Filters.Implementation;
using Surging.Core.CPlatform.Routing;
using Surging.Core.ProxyGenerator;
using Surging.Core.ProxyGenerator.Utilitys;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using GateWayAppConfig = Surging.Core.ApiGateWay.AppConfig;
using System.Reflection;
using Surging.Core.CPlatform.Utilities;
using Newtonsoft.Json.Linq;
using Surging.Core.CPlatform.Transport.Implementation;
using Surging.Core.CPlatform.Routing.Template;
using MessageStatusCode = Surging.Core.CPlatform.Messages.StatusCode;
using Surging.Core.CPlatform.Serialization;
using MicroService.Data.Validation;
using Surging.Core.ApiGateWay.Utilities;
using Newtonsoft.Json;
namespace Surging.ApiGateway.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceProxyProvider _serviceProxyProvider;
        private readonly IServiceRouteProvider _serviceRouteProvider;
        private readonly IAuthorizationServerProvider _authorizationServerProvider;

     
        public ServicesController(IServiceProxyProvider serviceProxyProvider, 
            IServiceRouteProvider serviceRouteProvider,
            IAuthorizationServerProvider authorizationServerProvider)
        {
            _serviceProxyProvider = serviceProxyProvider;
            _serviceRouteProvider = serviceRouteProvider;
            _authorizationServerProvider = authorizationServerProvider;
        }

        public async Task<Surging.Core.ApiGateWay.ServiceResult<object>> Path([FromServices]IServicePartProvider servicePartProvider, string path, [FromBody]Dictionary<string, object> model)
        {
            string serviceKey = this.Request.Query["servicekey"];
            path = path.IndexOf("/") < 0 ? $"/{path}" : path;
            if (model == null)
            {
                model = new Dictionary<string, object>();
            }
            foreach (string n in this.Request.Query.Keys)
            {
                model[n] = this.Request.Query[n].ToString();
            }
            Surging.Core.ApiGateWay.ServiceResult<object> result = Surging.Core.ApiGateWay.ServiceResult<object>.Create(false, null);
            path = String.Compare(path.ToLower(), GateWayAppConfig.TokenEndpointPath, true) == 0 ?
              GateWayAppConfig.AuthorizationRoutePath : path.ToLower();
            var route = await _serviceRouteProvider.GetRouteByPathRegex(path);
            var httpMethods = route.ServiceDescriptor.HttpMethod();
            if (!string.IsNullOrEmpty(httpMethods) &&
                !httpMethods.Contains(Request.Method))
                return new Surging.Core.ApiGateWay.ServiceResult<object> { IsSucceed = false, StatusCode = (int)ServiceStatusCode.Http405Endpoint, Message = "405 HTTP Method Not Supported" };
            if (!GetAllowRequest(route)) return new Surging.Core.ApiGateWay.ServiceResult<object> { IsSucceed = false, StatusCode = (int)ServiceStatusCode.RequestError, Message = "Request error" };
            if (servicePartProvider.IsPart(path))
            {
                //result = ServiceResult<object>.Create(true, await servicePartProvider.Merge(path, model));
                //result.StatusCode = (int)ServiceStatusCode.Success;
                var data = (string)await servicePartProvider.Merge(path, model);
                return AuthenticationCommon.CreateServiceResult(data);
            }
            else
            {
                var auth = await OnAuthorization(path, route, model);
                result = auth.result;
                if (auth.isSuccess)
                {
                    if (path == GateWayAppConfig.AuthorizationRoutePath)
                    {
                        var oAuthUser = await _authorizationServerProvider.GenerateTokenCredential(model);
                        if (oAuthUser.IsSucceed)
                        {
                            result =  Surging.Core.ApiGateWay.ServiceResult<object>.Create(true, oAuthUser);
                            result.StatusCode = (int)ServiceStatusCode.Success;
                        }
                        else
                        {
                            result =  new Surging.Core.ApiGateWay.ServiceResult<object> { IsSucceed = false, StatusCode = (int)ServiceStatusCode.AuthorizationFailed,Entity= oAuthUser };
                        }
                    }
                    else
                    {
                        if (String.Compare(route.ServiceDescriptor.RoutePath, path, true) != 0)
                        {
                            var pamars = RouteTemplateSegmenter.Segment(route.ServiceDescriptor.RoutePath, path);
                            foreach (KeyValuePair<string, object> item in pamars)
                            {
                                model.Add(item.Key, item.Value);
                            }
                        }
                        if (!string.IsNullOrEmpty(serviceKey))
                        {

                            //result = ServiceResult<object>.Create(true, await _serviceProxyProvider.Invoke<object>(model, route.ServiceDescriptor.RoutePath, serviceKey));
                            //result.StatusCode = (int)ServiceStatusCode.Success;
                            var data = await _serviceProxyProvider.Invoke<object>(model, path, serviceKey);
                            return AuthenticationCommon.CreateServiceResult(data);
                        }
                        else
                        {
                            //result = ServiceResult<object>.Create(true, await _serviceProxyProvider.Invoke<object>(model, route.ServiceDescriptor.RoutePath));
                            //result.StatusCode = (int)ServiceStatusCode.Success;
                            var data = await _serviceProxyProvider.Invoke<object>(model, path);
                            return AuthenticationCommon.CreateServiceResult(data);
                        }
                    }
                }
            }
            return result;
        }

        private bool GetAllowRequest(ServiceRoute route)
        {  
            return !route.ServiceDescriptor.DisableNetwork();
        }

        private async Task<AuthorServiceResult> OnAuthorization(string path, ServiceRoute route, Dictionary<string, object> model)
        {
            //  bool isSuccess = true;
            AuthorServiceResult authorServiceResult = new AuthorServiceResult();
            // var route = await _serviceRouteProvider.GetRouteByPath(path);
            if (route.ServiceDescriptor.EnableAuthorization())
            {
                if (route.ServiceDescriptor.AuthType() == AuthorizationType.JWT.ToString())
                {

                    authorServiceResult = await ValidateJwtAuthentication(route, path, model);
                }
                else
                {
                    authorServiceResult = await ValidateAppSecretAuthentication(route, path, model);
                }

            }
            return authorServiceResult;
        }
      
        public async Task<AuthorServiceResult> ValidateJwtAuthentication(ServiceRoute route, string path, Dictionary<string, object> model)
        {
            AuthorServiceResult authorServiceResult = new AuthorServiceResult();
            // bool isSuccess = true;
            var author = HttpContext.Request.Headers["Authorization"];

            if (author.Count > 0)
            {
                var token = AuthenticationCommon.GetAuthToken(author);
                authorServiceResult.isSuccess = await _authorizationServerProvider.ValidateClientAuthentication(token);
                if (!authorServiceResult.isSuccess)
                {
                    authorServiceResult.result = new Surging.Core.ApiGateWay.ServiceResult<object> { IsSucceed = false, StatusCode = (int)ServiceStatusCode.AuthorizationFailed, Message = "Invalid authentication credentials" };
                }
                else
                {
                    var onAuthorModel = new Dictionary<string, object>();
                    var payload = _authorizationServerProvider.GetPayloadString(token); ;
                    var keyValue = model.FirstOrDefault();
                    if (!(keyValue.Value is IConvertible) || !typeof(IConvertible).GetTypeInfo().IsAssignableFrom(keyValue.Value.GetType()))
                    {
                        dynamic instance = keyValue.Value;
                        instance.Payload = payload;
                        RpcContext.GetContext().SetAttachment("payload", instance.Payload.ToString());
                        model.Remove(keyValue.Key);
                        model.Add(keyValue.Key, instance);
                    }
                    //onAuthorModel.Add("input", JsonConvert.SerializeObject(new
                    //{
                    //    Path = path,
                    //    Payload = payload
                    //}));
                    //var data = await _serviceProxyProvider.Invoke<bool>(onAuthorModel, "api/user/onauthentication", "User");

                    //if (!data)
                    //{
                    //    authorServiceResult.isSuccess = false;
                    //    authorServiceResult.result = new Surging.Core.ApiGateWay.ServiceResult<object> { IsSucceed = false, StatusCode = (int)ServiceStatusCode.AuthorizationFailed, Message = "没有该操作权限" };
                    //}
                    //else
                    //{
                    //    var keyValue = model.FirstOrDefault();
                    //    if (!(keyValue.Value is IConvertible) || !typeof(IConvertible).GetTypeInfo().IsAssignableFrom(keyValue.Value.GetType()))
                    //    {
                    //        dynamic instance = keyValue.Value;
                    //        instance.Payload = payload;
                    //        RpcContext.GetContext().SetAttachment("payload", instance.Payload.ToString());
                    //        model.Remove(keyValue.Key);
                    //        model.Add(keyValue.Key, instance);
                    //    }
                    //}
                }


            }
            else
            {
                authorServiceResult.result = new Surging.Core.ApiGateWay.ServiceResult<object> { IsSucceed = false, StatusCode = (int)ServiceStatusCode.RequestError, Message = "Request error" };
                authorServiceResult.isSuccess = false;
            }
            return authorServiceResult;
        }


        private async Task<AuthorServiceResult> ValidateAppSecretAuthentication(ServiceRoute route,
            string path,
            Dictionary<string, object> model)
        {
            AuthorServiceResult authorServiceResult = new AuthorServiceResult();
            var author = HttpContext.Request.Headers["Authorization"];
            if (author.Count > 0)
            {
                var token = AuthenticationCommon.GetAuthToken(author);
                authorServiceResult.isSuccess = await _authorizationServerProvider.ValidateClientAuthentication(token);
                if (!authorServiceResult.isSuccess)
                {
                    authorServiceResult.result = new Surging.Core.ApiGateWay.ServiceResult<object> { IsSucceed = false, StatusCode = (int)ServiceStatusCode.AuthorizationFailed, Message = "Invalid authentication credentials" };
                }
                else
                {
                    var keyValue = model.FirstOrDefault();
                    if (!(keyValue.Value is IConvertible) || !typeof(IConvertible).GetTypeInfo().IsAssignableFrom(keyValue.Value.GetType()))
                    {
                        dynamic instance = keyValue.Value;
                        instance.Payload = _authorizationServerProvider.GetPayloadString(token);
                        RpcContext.GetContext().SetAttachment("payload", instance.Payload.ToString());
                        model.Remove(keyValue.Key);
                        model.Add(keyValue.Key, instance);
                    }
                }
            }
            else
            {
                authorServiceResult.result = new Surging.Core.ApiGateWay.ServiceResult<object> { IsSucceed = false, StatusCode = (int)ServiceStatusCode.RequestError, Message = "Request error" };
                authorServiceResult.isSuccess = false;
            }
            return authorServiceResult;
         
        }

        public static string GetMD5(string encypStr)
        {
            try
            {
                var md5 = MD5.Create();
                var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(encypStr));
                var sb = new StringBuilder();
                foreach (byte b in bs)
                {
                    sb.Append(b.ToString("X2"));
                }
                //所有字符转为大写
                return sb.ToString().ToLower();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}

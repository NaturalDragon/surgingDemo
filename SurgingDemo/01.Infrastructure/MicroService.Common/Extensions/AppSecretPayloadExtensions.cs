using MicroService.Common.Author;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Common.Extensions
{
  public static  class AppSecretPayloadExtensions
    {
        public static void InitTokenInfo(this BaseRequest  baseRequest)
        {
            TokenInfo  info = GetLoginUser(baseRequest.Payload);


            baseRequest.AppKey = info.AppKey;
            baseRequest.AppSecret = info.AppSecret;
        }
        private static TokenInfo GetLoginUser(string payLoad)
        {
            var token = payLoad.Replace("\\", "").Trim('"');
            var login = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenInfo>(token);
            return login;
        }

    }
}

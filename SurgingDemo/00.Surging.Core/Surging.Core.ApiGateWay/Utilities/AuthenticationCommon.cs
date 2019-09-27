using Surging.Core.CPlatform.Serialization;
using Surging.Core.CPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageStatusCode = Surging.Core.CPlatform.Messages.StatusCode;
namespace Surging.Core.ApiGateWay.Utilities
{
   public class AuthenticationCommon
    {
        public static string GetAuthToken(IEnumerable<string> headerValues)
        {
            string authorization = headerValues.First();
            string token = null;

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }

            return token;
        }
        public static Surging.Core.ApiGateWay.ServiceResult<object> CreateServiceResult(object data)
        {
            if (data == null)
            {
                var serviceResult = Surging.Core.ApiGateWay.ServiceResult<object>.Create(true, data);
                serviceResult.StatusCode = (int)MessageStatusCode.CPlatformError;
                return serviceResult;
            }

            if (data.GetType() == typeof(string))
            {
                var dataStr = (string)data;
                if (dataStr.IsValidJson())
                {
                    var serializer = ServiceLocator.GetService<ISerializer<string>>();
                    var dataObj = serializer.Deserialize(dataStr, typeof(object), true);
                    var serviceResult = Surging.Core.ApiGateWay.ServiceResult<object>.Create(true, dataObj);
                    serviceResult.StatusCode = (int)MessageStatusCode.OK;
                    return serviceResult;
                }
                else
                {
                    var serviceResult = Surging.Core.ApiGateWay.ServiceResult<object>.Create(true, data);
                    serviceResult.StatusCode = (int)MessageStatusCode.OK;
                    return serviceResult;

                }
            }
            else
            {
                var serviceResult = Surging.Core.ApiGateWay.ServiceResult<object>.Create(true, data);
                serviceResult.StatusCode = (int)MessageStatusCode.OK;
                return serviceResult;

            }
        }


    }
}

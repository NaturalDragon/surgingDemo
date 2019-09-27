using Surging.Core.CPlatform.Filters.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.ServerHost.Org.Filter
{
   public class ServiceExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(RpcActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            Console.WriteLine(actionExecutedContext.InvokeMessage);
        }
    }
}

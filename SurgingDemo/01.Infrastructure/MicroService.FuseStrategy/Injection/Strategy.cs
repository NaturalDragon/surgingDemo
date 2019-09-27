using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.FuseStrategy.Injection
{
   public  class Strategy
    {

        public const string BaseStrategy = @"MicroService.Data.Validation.JsonResponse jsonRes = new MicroService.Data.Validation.JsonResponse();jsonRes.Errors.AddError("""", """", errorMessage: ""系统繁忙，请稍后重试！"");return jsonRes;";

        public const string MessageStrategy = @"return ""系统繁忙，请稍后重试！"" ;";

    }
}

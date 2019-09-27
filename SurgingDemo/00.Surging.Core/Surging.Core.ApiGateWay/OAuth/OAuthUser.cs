using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Core.ApiGateWay.OAuth
{
  public  class OAuthUser
    {

        public bool IsSucceed { set; get; }

        public string Message { set; get; }
        public string AccessToken { set; get; }

        public double ExpireTime { set; get; }
    }
}

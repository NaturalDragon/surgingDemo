using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Core.ApiGateWay.Utilities
{
    public class AuthorServiceResult
    {
        public bool isSuccess { set; get; } = true;

        public Surging.Core.ApiGateWay.ServiceResult<object> result { set; get; }
    }
}

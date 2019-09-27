using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Common.Author
{
   public class BaseRequest: RequestData
    {
        public string AppKey { set; get; }

        public string AppSecret { set; get; }


        public string OrgId { set; get;
        }
    }
}

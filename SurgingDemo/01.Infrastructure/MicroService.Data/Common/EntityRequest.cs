using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Common
{
   public class EntityRequest:LoginUser
    {
        public List<string> Ids { set; get; }
    }
}

using MicroService.Common.Models;
using System.Collections.Generic;

namespace MicroService.Data.Common
{
    public class EntityQueryRequest:LoginUser
    {
       

        public IList<string> Ids { set; get; }

      

      


    }
}

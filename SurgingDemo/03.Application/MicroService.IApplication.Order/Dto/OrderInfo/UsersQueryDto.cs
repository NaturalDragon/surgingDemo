using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.IApplication.Order.Dto
{
    public class UsersQueryDto : BaseDto
    {
        //<summary>
        // 
        //<summary>
        public string Name { set; get; }


        public string RoleName { set; get; }

        //<summary>
        // 
        //<summary>
        public string RoleId { set; get; }

        //<summary>
        // 
        //<summary>
        public string PhoneCode { set; get; }

        //<summary>
        // 
        //<summary>
        public string Password { set; get; }
    }
}

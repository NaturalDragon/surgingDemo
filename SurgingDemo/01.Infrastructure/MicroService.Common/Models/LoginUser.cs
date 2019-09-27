using MicroService.Common.Core.Enums;
using System;

namespace MicroService.Common.Models
{
    public class LoginUser: BaseDto
    {
        public  DateTime ExpireDateTime { set; get; }

        public bool IsSucceed { set; get; }

        public string Message { set; get; }


        public RoleType RoleType { set; get; }
        public string RoleId { set; get; }

        public string UserId { set; get; }

        public string Account { set; get; }
        public string Name { set; get; }

        public string Password { set; get; }

        public string PhoneCode{  set; get; }


        public string Key { set; get; }

        /// <summary>
        /// 所属公司Id
        /// </summary>
        public string OrgId
        {
            get;
            set;
        }
     
    }
}

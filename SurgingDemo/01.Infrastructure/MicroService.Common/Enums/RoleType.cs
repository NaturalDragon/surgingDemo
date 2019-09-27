using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Common.Core.Enums
{
    public enum RoleType
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        Admin=0,
        /// <summary>
        /// 租户
        /// </summary>
        Tenant=1,

        /// <summary>
        /// 机构
        /// </summary>
        Organization=2,

        /// <summary>
        /// 
        /// </summary>
        None=3
    }
}


using MicroService.Core;
using System;
using System.Collections.Generic;
using System.Text;
using MicroService.Entity.Org;
using MicroService.IRespository.Org;
using MicroService.EntityFramwork;
using MicroService.Data;
 namespace MicroService.Respository.Org
 {
	/// <summary>
	/// Roles -仓储
	/// </summary>
     public class RolesRespository : RespositoryBase<Roles>, IRolesRespository
    {
        public RolesRespository()
        {
        }
    }
}
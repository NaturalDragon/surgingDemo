
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
	/// Organization -仓储
	/// </summary>
     public class OrganizationRespository : RespositoryBase<Organization>, IOrganizationRespository
    {
        public OrganizationRespository()
        {
        }
    }
}
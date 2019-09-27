
using MicroService.Core;
using MicroService.Data;
using System;
using System.Collections.Generic;
using System.Text;
using MicroService.Entity.Org;
 namespace MicroService.IRespository.Org
 {
	/// <summary>
	/// Organization -仓储接口
	/// </summary>
	public interface IOrganizationRespository:IRespositoryBase<Organization>, IDependency
	{
  
        
	}
}
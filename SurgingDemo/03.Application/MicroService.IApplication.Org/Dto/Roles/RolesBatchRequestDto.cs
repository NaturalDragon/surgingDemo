
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Org.Dto
 {
	/// <summary>
	/// Roles --dto
	/// </summary>
    [Serializable]
	public class RolesBatchRequestDto:LoginUser
	{
	      public List<RolesRequestDto> RolesRequestList { set; get; }
	}
}
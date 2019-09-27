using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Domain
 {
	/// <summary>
	/// Users --实体
	/// </summary>
	public class Users:Entity<string>
	{
  		//<summary>
		// 
		//<summary>
		[StringLength(64)]
		[Required]
		public string Name { set; get; }
		//<summary>
		// 
		//<summary>
		[StringLength(36)]
		[Required]
		public string RoleId { set; get; }
		//<summary>
		// 
		//<summary>
		[StringLength(16)]
			public string PhoneCode { set; get; }
		//<summary>
		// 
		//<summary>
		[StringLength(128)]
		[Required]
		public string Password { set; get; }
 
	}
}
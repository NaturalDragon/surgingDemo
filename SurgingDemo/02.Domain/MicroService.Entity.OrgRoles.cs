using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Domain
 {
	/// <summary>
	/// Roles --实体
	/// </summary>
	public class Roles:Entity<string>
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
		[Required]
		public int Level { set; get; }
		//<summary>
		// 
		//<summary>
		[StringLength(64)]
		[Required]
		public string Name { set; get; }
 
	}
}
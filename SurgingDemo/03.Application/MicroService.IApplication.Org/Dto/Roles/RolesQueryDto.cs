using MicroService.Core;
using MicroService.Data.Enums;
using MicroService.Common.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.IApplication.Org.Dto
 {
	/// <summary>
	/// Roles --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class RolesQueryDto:BaseDto
	{

		//<summary>
		// 
		//<summary>
		public string Name { set; get; }

		//<summary>
		// 
		//<summary>
		public int Level { set; get; }

 
	}
}
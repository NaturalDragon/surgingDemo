
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Enums;
using MicroService.Common.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.IApplication.Org.Dto
 {
	/// <summary>
	/// Organization --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class OrganizationRequestDto:LoginUser
	{

		//<summary>
		// 
		//<summary>
		public string Name { set; get; }

		//<summary>
		// 
		//<summary>
		public string ParentId { set; get; }
 
	}
}
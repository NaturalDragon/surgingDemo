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
	/// Users --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class UsersQueryDto:BaseDto
	{

		//<summary>
		// 
		//<summary>
		public string Name { set; get; }


        public string RoleName { set; get; }

        //<summary>
        // 
        //<summary>
        public string RoleId { set; get; }

		//<summary>
		// 
		//<summary>
		public string PhoneCode { set; get; }

		//<summary>
		// 
		//<summary>
		public string Password { set; get; }
 
	}
}
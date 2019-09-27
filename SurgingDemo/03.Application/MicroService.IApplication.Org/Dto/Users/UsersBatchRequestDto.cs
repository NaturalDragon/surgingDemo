
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Org.Dto
 {
	/// <summary>
	/// Users --dto
	/// </summary>
    [Serializable]
	public class UsersBatchRequestDto:LoginUser
	{
	      public List<UsersRequestDto> UsersRequestList { set; get; }
	}
}
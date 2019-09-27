
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Order.Dto
 {
	/// <summary>
	/// OrderDetail --dto
	/// </summary>
    [Serializable]
	public class OrderDetailBatchRequestDto:LoginUser
	{
	      public List<OrderDetailRequestDto> OrderDetailRequestList { set; get; }
	}
}
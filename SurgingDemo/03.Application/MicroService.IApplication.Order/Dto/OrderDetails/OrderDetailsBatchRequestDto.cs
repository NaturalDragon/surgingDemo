
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Order.Dto
 {
	/// <summary>
	/// OrderDetails --dto
	/// </summary>
    [Serializable]
	public class OrderDetailsBatchRequestDto:LoginUser
	{
	      public List<OrderDetailsRequestDto> OrderDetailsRequestList { set; get; }
	}
}
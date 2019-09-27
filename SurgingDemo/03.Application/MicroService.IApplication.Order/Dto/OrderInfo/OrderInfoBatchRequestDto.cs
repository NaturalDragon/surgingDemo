
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Order.Dto
 {
	/// <summary>
	/// OrderInfo --dto
	/// </summary>
    [Serializable]
	public class OrderInfoBatchRequestDto:LoginUser
	{
	      public List<OrderInfoRequestDto> OrderInfoRequestList { set; get; }
	}
}
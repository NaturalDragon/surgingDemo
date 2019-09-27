
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Order.Dto
 {
	/// <summary>
	/// OrderInfos --dto
	/// </summary>
    [Serializable]
	public class OrderInfosBatchRequestDto:LoginUser
	{
	      public List<OrderInfosRequestDto> OrderInfosRequestList { set; get; }
	}
}
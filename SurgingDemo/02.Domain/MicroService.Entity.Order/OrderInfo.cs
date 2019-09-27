using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Order
 {
	/// <summary>
	/// OrderInfo --实体
	/// </summary>
	public class OrderInfo:Entity<string>
	{
  		//<summary>
		// 订单号
		//<summary>
		[StringLength(128)]
		[Required]
		public string OrderNumber { set; get; }
		//<summary>
		// 总金额
		//<summary>
		[Required]
		public decimal TotalMoney { set; get; }
		//<summary>
		// 下单用户
		//<summary>
		[StringLength(36)]
		[Required]
		public string UserId { set; get; }
		//<summary>
		// 过期时间
		//<summary>
		[Required]
		public DateTime ExpireTime { set; get; }
		//<summary>
		// 订单状态
		//<summary>
		[Required]
		public int Status { set; get; }
 
	}
}
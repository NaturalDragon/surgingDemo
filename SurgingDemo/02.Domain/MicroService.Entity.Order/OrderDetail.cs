using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Order
 {
	/// <summary>
	/// OrderDetail --实体
	/// </summary>
	public class OrderDetail:Entity<string>
	{
  		//<summary>
		// 订单id
		//<summary>
		[StringLength(36)]
		[Required]
		public string OrderId { set; get; }
		//<summary>
		// 商品id
		//<summary>
		[StringLength(36)]
		[Required]
		public string GoodsId { set; get; }
		//<summary>
		// 单价
		//<summary>
		[Required]
		public decimal Price { set; get; }
		//<summary>
		// 数量
		//<summary>
		[Required]
		public int Count { set; get; }
		//<summary>
		// 合计
		//<summary>
        [Required]
			public decimal Money { set; get; }
 
	}
}
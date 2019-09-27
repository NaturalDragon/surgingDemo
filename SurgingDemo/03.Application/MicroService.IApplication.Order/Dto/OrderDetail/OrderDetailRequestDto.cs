
using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Enums;
using MicroService.Common.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.IApplication.Order.Dto
 {
	/// <summary>
	/// OrderDetail --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class OrderDetailRequestDto:LoginUser
	{

		//<summary>
		// 订单id
		//<summary>
		public string OrderId { set; get; }

		//<summary>
		// 商品id
		//<summary>
		public string GoodsId { set; get; }

		//<summary>
		// 单价
		//<summary>
		public decimal Price { set; get; }

		//<summary>
		// 数量
		//<summary>
		public int Count { set; get; }

		//<summary>
		// 合计
		//<summary>
		public decimal Money { set; get; }
 
	}
}
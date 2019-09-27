using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.TestTable
 {
	/// <summary>
	/// TestTable --实体
	/// </summary>
	public class TestTable:Entity<string>
	{
  


	 //<summary>
     // 订单id
     //<summary>
		[StringLength(36)]
			[Required]
	    public  String  OrderId { set; get; }

	 //<summary>
     // 商品id
     //<summary>
		[StringLength(36)]
			[Required]
	    public  String  GoodsId { set; get; }

	 //<summary>
     // 单价
     //<summary>
			[Required]
	    public  Decimal  Price { set; get; }

	 //<summary>
     // 数量
     //<summary>
			[Required]
	    public  Int32  Count { set; get; }

	 //<summary>
     // 合计
     //<summary>
		    public  Decimal?  Money { set; get; }
 
	}
}
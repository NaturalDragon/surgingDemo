using MicroService.Core;
using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace MicroService.Entity.Product
 {
	/// <summary>
	/// Goods --实体
	/// </summary>
	public class Goods:Entity<string>
	{
  		//<summary>
		// 商品名称
		//<summary>
		[StringLength(128)]
		[Required]
		public string Name { set; get; }
		//<summary>
		// 库存
		//<summary>
		[Required]
		public int StockNum { set; get; }
		//<summary>
		// 单价
		//<summary>
		[Required]
		public decimal Price { set; get; }
		//<summary>
		// 封面图
		//<summary>
		[StringLength(256)]
		[Required]
		public string CoverImgSrc { set; get; }
		//<summary>
		// 详情
		//<summary>
		[Required]
		public string Details { set; get; }
 
	}
}
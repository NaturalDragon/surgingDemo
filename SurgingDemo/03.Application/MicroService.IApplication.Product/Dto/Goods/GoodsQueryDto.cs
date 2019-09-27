using MicroService.Core;
using MicroService.Data.Enums;
using MicroService.Common.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.IApplication.Product.Dto
 {
	/// <summary>
	/// Goods --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class GoodsQueryDto:BaseDto
	{

		//<summary>
		// 商品名称
		//<summary>
		public string Name { set; get; }

		//<summary>
		// 库存
		//<summary>
		public int StockNum { set; get; }

		//<summary>
		// 单价
		//<summary>
		public decimal Price { set; get; }

		//<summary>
		// 封面图
		//<summary>
		public string CoverImgSrc { set; get; }

		//<summary>
		// 详情
		//<summary>
		public string Details { set; get; }
 
	}
}
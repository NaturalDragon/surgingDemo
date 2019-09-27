
using MicroService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Product.Dto
 {
	/// <summary>
	/// Goods --dto
	/// </summary>
    [Serializable]
	public class GoodsBatchRequestDto:LoginUser
	{
	      public List<GoodsRequestDto> GoodsRequestList { set; get; }
	}
}
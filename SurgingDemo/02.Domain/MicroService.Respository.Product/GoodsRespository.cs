
using MicroService.Core;
using System;
using System.Collections.Generic;
using System.Text;
using MicroService.Entity.Product;
using MicroService.IRespository.Product;
using MicroService.EntityFramwork;
using MicroService.Data;
 namespace MicroService.Respository.Product
 {
	/// <summary>
	/// Goods -仓储
	/// </summary>
     public class GoodsRespository : RespositoryBase<Goods>, IGoodsRespository
    {
        public GoodsRespository()
        {
        }
    }
}
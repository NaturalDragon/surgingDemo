
using MicroService.Core;
using System;
using System.Collections.Generic;
using System.Text;
using MicroService.Entity.Order;
using MicroService.IRespository.Order;
using MicroService.EntityFramwork;
using MicroService.Data;
 namespace MicroService.Respository.Order
 {
	/// <summary>
	/// OrderDetail -仓储
	/// </summary>
     public class OrderDetailRespository : RespositoryBase<OrderDetail>, IOrderDetailRespository
    {
        public OrderDetailRespository()
        {
        }
    }
}
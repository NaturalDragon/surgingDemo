using MicroService.IApplication.Order.Dto;
using Newtonsoft.Json;
using Surging.Core.CPlatform.Transport.Implementation;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Modules.Order
{
   public class RpcService
    {
        public static async Task<List<GoodsRequestDto>> GetGoodsAsync(OrderInfoRequestDto orderInfoRequestDto)
        {
            var serviceProxyProvider = ServiceLocator.GetService<IServiceProxyProvider>();
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("entityQueryRequest", JsonConvert.SerializeObject(new
            {
                Ids = orderInfoRequestDto.GoodsRequestDtos.Select(g => g.Id).ToList(),
            }));
            string path = "api/Goods/GetGoodsByIds";
            string serviceKey = "Goods";

            var goodsProxy = await serviceProxyProvider.Invoke<object>(model, path, serviceKey);
            List<GoodsRequestDto> goodsQuerys = JsonConvert.DeserializeObject<List<GoodsRequestDto>>(goodsProxy.ToString());
            return goodsQuerys;
        }


        public static async Task<UsersQueryDto> GetUserByIdAsync(string userId,string payload)
        {
            var serviceProxyProvider = ServiceLocator.GetService<IServiceProxyProvider>();
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("input", JsonConvert.SerializeObject(new
            {
                id = userId
            }));
            string path = "api/Users/GetForModify";
            string serviceKey = "Users";
            RpcContext.GetContext().SetAttachment("payload", payload);
            var resultProxy = await serviceProxyProvider.Invoke<object>(model, path, serviceKey);
            UsersQueryDto resultQuerys = JsonConvert.DeserializeObject< UsersQueryDto > (resultProxy.ToString());
            return resultQuerys;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Application.Order.Validators;
using MicroService.Data.Extensions;
using MicroService.Data.Validation;
using MicroService.Entity.Order;
using MicroService.IRespository.Order;

namespace MicroService.Application.Order.ValidatorsFilters
{
    public class OrderInfoValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrderInfoRespository orderInfoRespository,OrderInfo orderInfo, string validatorType)
        {
            var orderInfoValidator = new OrderInfoValidator(orderInfoRespository);
            var validatorReresult = await orderInfoValidator.DoValidateAsync(orderInfo, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrderInfoRespository orderInfoRespository,IEnumerable<OrderInfo> orderInfos, string validatorType)
        {
            var orderInfoValidator = new OrderInfoValidator(orderInfoRespository);
            var domainException = new DomainException();
            foreach (var orderInfo in orderInfos)
            {
                var validatorReresult = await orderInfoValidator.DoValidateAsync(orderInfo, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}
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
    public class OrderDetailValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrderDetailRespository orderDetailRespository,OrderDetail orderDetail, string validatorType)
        {
            var orderDetailValidator = new OrderDetailValidator(orderDetailRespository);
            var validatorReresult = await orderDetailValidator.DoValidateAsync(orderDetail, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrderDetailRespository orderDetailRespository,IEnumerable<OrderDetail> orderDetails, string validatorType)
        {
            var orderDetailValidator = new OrderDetailValidator(orderDetailRespository);
            var domainException = new DomainException();
            foreach (var orderDetail in orderDetails)
            {
                var validatorReresult = await orderDetailValidator.DoValidateAsync(orderDetail, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}
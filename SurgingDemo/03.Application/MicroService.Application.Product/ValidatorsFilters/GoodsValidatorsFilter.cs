using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Application.Product.Validators;
using MicroService.Data.Extensions;
using MicroService.Data.Validation;
using MicroService.Entity.Product;
using MicroService.IRespository.Product;

namespace MicroService.Application.Product.ValidatorsFilters
{
    public class GoodsValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IGoodsRespository goodsRespository,Goods goods, string validatorType)
        {
            var goodsValidator = new GoodsValidator(goodsRespository);
            var validatorReresult = await goodsValidator.DoValidateAsync(goods, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IGoodsRespository goodsRespository,IEnumerable<Goods> goodss, string validatorType)
        {
            var goodsValidator = new GoodsValidator(goodsRespository);
            var domainException = new DomainException();
            foreach (var goods in goodss)
            {
                var validatorReresult = await goodsValidator.DoValidateAsync(goods, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}
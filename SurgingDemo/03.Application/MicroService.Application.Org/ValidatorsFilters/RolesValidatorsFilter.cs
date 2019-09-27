using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Application.Org.Validators;
using MicroService.Data.Extensions;
using MicroService.Data.Validation;
using MicroService.Entity.Org;
using MicroService.IRespository.Org;

namespace MicroService.Application.Org.ValidatorsFilters
{
    public class RolesValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRolesRespository rolesRespository,Roles roles, string validatorType)
        {
            var rolesValidator = new RolesValidator(rolesRespository);
            var validatorReresult = await rolesValidator.DoValidateAsync(roles, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IRolesRespository rolesRespository,IEnumerable<Roles> roless, string validatorType)
        {
            var rolesValidator = new RolesValidator(rolesRespository);
            var domainException = new DomainException();
            foreach (var roles in roless)
            {
                var validatorReresult = await rolesValidator.DoValidateAsync(roles, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}
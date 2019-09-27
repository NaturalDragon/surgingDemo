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
    public class OrganizationValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrganizationRespository organizationRespository,Organization organization, string validatorType)
        {
            var organizationValidator = new OrganizationValidator(organizationRespository);
            var validatorReresult = await organizationValidator.DoValidateAsync(organization, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IOrganizationRespository organizationRespository,IEnumerable<Organization> organizations, string validatorType)
        {
            var organizationValidator = new OrganizationValidator(organizationRespository);
            var domainException = new DomainException();
            foreach (var organization in organizations)
            {
                var validatorReresult = await organizationValidator.DoValidateAsync(organization, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}
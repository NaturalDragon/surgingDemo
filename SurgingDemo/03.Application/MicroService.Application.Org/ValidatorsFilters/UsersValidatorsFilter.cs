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
    public class UsersValidatorsFilter
    {
          /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IUsersRespository usersRespository,Users users, string validatorType)
        {
            var usersValidator = new UsersValidator(usersRespository);
            var validatorReresult = await usersValidator.DoValidateAsync(users, validatorType);
            if (!validatorReresult.IsValid)
            {
                throw new DomainException(validatorReresult);
            }
        }

        /// <summary>
        ///异步验证
        /// </summary>
        public static async Task DoValidationAsync(IUsersRespository usersRespository,IEnumerable<Users> userss, string validatorType)
        {
            var usersValidator = new UsersValidator(usersRespository);
            var domainException = new DomainException();
            foreach (var users in userss)
            {
                var validatorReresult = await usersValidator.DoValidateAsync(users, validatorType);
                if (!validatorReresult.IsValid)
                {
                    domainException.AddErrors(validatorReresult);
                }

            }

            if (domainException.ValidationErrors.ErrorItems.Any()) throw domainException;
        }
    }
}
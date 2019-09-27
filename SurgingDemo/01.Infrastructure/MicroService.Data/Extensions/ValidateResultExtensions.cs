using FluentValidation;
using FluentValidation.Results;
using MicroService.Data.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Data.Extensions
{
    public static class ValidateResultExtensions
    {
        public static ValidationErrors ToValidationError(this ValidationResult validationResult)
        {
            var validationError = new ValidationErrors();

            foreach (var failure in validationResult.Errors)
            {
                validationError.AddError(failure.PropertyName, failure.AttemptedValue, failure.ErrorMessage, false);
            }

            return validationError;
        }

        public static ValidationErrors DoValidate<T>(this IValidator<T> validator, T instance, string ruleSet = null)
        {
            var validationResult = validator.Validate(instance, ruleSet: ruleSet);

            var validationError = new ValidationErrors();

            foreach (var failure in validationResult.Errors)
            {
                validationError.AddError(failure.PropertyName, failure.AttemptedValue, failure.ErrorMessage, false);
            }

            return validationError;
        }

        public static async Task<ValidationErrors> DoValidateAsync<T>(this IValidator<T> validator, T instance, string ruleSet = null, object validateKey = null)
        {
            var validationResult = await validator.ValidateAsync(instance, ruleSet: ruleSet);

            var validationError = new ValidationErrors();
            validateKey = validateKey ?? string.Empty;

            foreach (var failure in validationResult.Errors)
            {
                validationError.AddError(failure.PropertyName, failure.AttemptedValue, failure.ErrorMessage, false, validateKey.ToString());
            }

            return validationError;
        }

    }
}

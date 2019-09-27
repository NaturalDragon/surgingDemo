using MicroService.Data.Enums;
using MicroService.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MicroService.Data.Validation
{
    /// <summary>
    /// 业务逻辑异常
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        public ValidationErrors ValidationErrors { get; private set; }

        public DomainException()
        {
            ValidationErrors = new ValidationErrors();
        }

        public DomainException(ValidationErrors validationErrors, ValidationErrorType errorType = ValidationErrorType.Body)
        {
            ValidationErrors = validationErrors;
            foreach (var validationError in ValidationErrors.ErrorItems)
            {
                validationError.ErrorType = EnumUtility.GetDescriptions(errorType);
            }
        }

        public void AddErrors(ValidationErrors validationErrors)
        {
            ValidationErrors.AddErrors(validationErrors.ErrorItems);
        }

        public ValidationErrors AddError<TObject, TProperty>(Expression<Func<TObject, TProperty>> expression, object attemptedValue, string errorMessage, bool customState = false)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
                return ValidationErrors.AddError(memberExpression.Member.Name, errorMessage, attemptedValue, customState);
            return new ValidationErrors();
        }

        public static TException Create<TException>(ValidationErrors errors) where TException : DomainException, new()
        {
            var exception = new TException();
            if (errors != null)
            {
                exception.ValidationErrors = errors;
            }

            return exception;
        }
    }
}

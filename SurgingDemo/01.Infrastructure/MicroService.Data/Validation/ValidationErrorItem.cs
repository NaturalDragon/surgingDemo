using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Validation
{
    public class ValidationErrorItem
    {
        public ValidationErrorItem(string propertyName, string errorMessage, object attemptedValue, bool customState, string errorKey = "")
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            AttemptedValue = attemptedValue;
            CustomState = customState;
            ErrorKey = errorKey;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// The property value that caused the failure.
        /// </summary>
        public object AttemptedValue { get; private set; }

        /// <summary>
        /// Custom state associated with the failure.
        /// </summary>
        public bool CustomState { get; set; }

        public string ErrorType { get; set; }

        public string ErrorKey { get; set; }
    }
}

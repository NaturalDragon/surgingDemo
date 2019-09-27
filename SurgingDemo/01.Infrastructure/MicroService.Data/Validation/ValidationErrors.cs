using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Validation
{
    [Serializable]
    public class ValidationErrors 
    {

        private readonly List<ValidationErrorItem> _errorItemList = new List<ValidationErrorItem>();

        public bool IsValid
        {
            get
            {
                return _errorItemList.Count == 0;
            }
        }

        public IEnumerable<ValidationErrorItem> ErrorItems { get { return _errorItemList; } }

        public ValidationErrors AddSystemError(string propertyName = InfrastructureContant.SystemError, 
            
            string errorMessage = InfrastructureContant.SystemErrorDescription,
            object attemptedValue = null, bool customState = false)
        {
            return AddError(propertyName, errorMessage, attemptedValue, customState);
        }

        public ValidationErrors AddErrors(IEnumerable<ValidationErrorItem> errorItems)
        {
            if (errorItems == null)
            {
                return this;
            }
            foreach (var errorItem in errorItems)
            {
                if (errorItem.CustomState)
                {
                    errorItem.PropertyName = "IsAlter";
                }
                _errorItemList.Add(errorItem);
            }

            return this;
        }
        public ValidationErrors AddError(string propertyName, object attemptedValue, string errorMessage = null, bool customState = false, string validateKey = "")
        {
            var errorItem = new ValidationErrorItem(propertyName, errorMessage, attemptedValue, customState, validateKey);
            _errorItemList.Add(errorItem);
            return this;
        }

        public ValidationErrors AddError(string propertyName, string errorMessage, object attemptedValue, bool customState = false)
        {
            var errorItem = new ValidationErrorItem(propertyName, errorMessage, attemptedValue, customState);
            _errorItemList.Add(errorItem);
            return this;
        }
    }
}

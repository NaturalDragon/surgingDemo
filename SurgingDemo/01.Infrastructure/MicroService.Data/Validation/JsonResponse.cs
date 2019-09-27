using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Validation
{
    public class JsonResponse : IJsonResponse
    {
        /// <summary>
        /// 错误集合
        /// </summary>
        /// <value>The errors.</value>
        public ValidationErrors Errors { get; set; }

        /// <summary>
        /// 系统错误信息
        /// </summary>

        public string SystemErrorMessage { get; set; }

        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                return Errors == null || Errors.IsValid;
            }
        }

        /// <summary>
        /// Gets the error property.
        /// </summary>
        /// <value>The error property.</value>
        public string ErrorProperty
        {
            get
            {
                if (Errors != null && !IsValid)
                {
                    foreach (ValidationErrorItem validationErrorItem in Errors.ErrorItems)
                    {
                        if (validationErrorItem.PropertyName == "IsAlter")
                        {
                            return "IsAlter";
                        }
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResponse"/> class.
        /// </summary>
        public JsonResponse()
        {
            Errors = new ValidationErrors();
        }

        /// <summary>
        /// Gets the error messages.
        /// </summary>
        /// <value>The error messages.</value>
        public string ErrorMessages
        {
            get
            {
                var errorMessages = new StringBuilder();
                if (Errors != null && !IsValid)
                {
                    foreach (ValidationErrorItem validationErrorItem in Errors.ErrorItems)
                    {
                        errorMessages.AppendFormat("{0}", validationErrorItem.ErrorMessage);
                    }
                }
                return errorMessages.ToString();
            }
        }

        /// <summary>
        /// 跳转路径
        /// </summary>
        /// <value>The redirect URL.</value>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 实体Id
        /// </summary>
        /// <value>The entity identifier.</value>
        public Guid EntityId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }


        public object Data { set; get; }
        public string Token { set; get; }
        /// <summary>
        /// 错误类型
        /// </summary>
        /// <value>The type of the error.</value>
        public ErrorType ErrorType { get; set; }

        public static JsonResponse Create(bool isValid)
        {
            var jsonResponse = new JsonResponse();
            if (!isValid)
            {
                jsonResponse.Errors.AddSystemError();
            }
            return jsonResponse;
        }
    }
}

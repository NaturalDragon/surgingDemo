using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Enums
{
    /// <summary>
    /// 错误类型
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// 验证错误
        /// </summary>
        ValidateError = 0,

        /// <summary>
        /// Token过期
        /// </summary>
        TokenExpire = 1
    }
}

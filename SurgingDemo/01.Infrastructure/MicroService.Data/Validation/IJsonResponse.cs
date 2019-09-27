using MicroService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Validation
{
    public interface IJsonResponse
    {
        /// <summary>
        /// 错误集合
        /// </summary>
        ValidationErrors Errors { get; set; }

        /// <summary>
        /// 跳转路径
        /// </summary>
        string RedirectUrl { get; set; }

        /// <summary>
        /// 是否验证通过
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// 实体Id
        /// </summary>
        Guid EntityId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// 错误类型
        /// </summary>
        ErrorType ErrorType { get; set; }
    }
}

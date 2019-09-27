using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Common.Core.Enums
{
    public enum OperationModel
    {

        /// <summary>
        /// 不改变
        /// </summary>
        Unchanged=0,
        /// <summary>
        /// 新增
        /// </summary>
        Create =1,
        /// <summary>
        /// 修改
        /// </summary>
        Modify=2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete=3
    }
}

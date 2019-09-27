using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MicroService.Data.Enums
{
    /// <summary>
    /// 枚举类---订单状态
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 未支付
        /// </summary>
        [Description("未支付")]
        NoPaid = 0,
        /// <summary>
        /// 已支付
        /// </summary>
        [Description("已支付")]
        Paid = 1,
        /// <summary>
        /// 申请退款
        /// </summary>
        [Description("申请退款")]
        ApplyRefund = 2,
        /// <summary>
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refund = 3,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Disabled = 4,

        /// <summary>
        /// 拒绝退款
        /// </summary>
        [Description("拒绝退款")]
        NoPass = 5,

        /// <summary>
        /// 已提现
        /// </summary>
        [Description("已提现")]
        Withdrawals = 6
    }
}

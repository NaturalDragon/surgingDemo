using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Constant
{
    public static class ErrorMessage
    {

        public const string Empty = "不能为空";

        public const string IsNameRequired = "名称不能为空";

        public const string IsNameRepeat = "名称已重复";

        public const string IsChildNodeExsit = "存在子节点";

        public const string IsSortIndexRequired = "排序不能为空";

        public const string IsAccountRequired = "帐号不能为空";

        public const string IsAccountRepeat = "帐号已重复";

        public const string IsCodeRequired = "编号不能为空";

        public const string IsCodeRepeat = "编号已重复";

        public const string IsPasswordRequired = "密码不能为空";

        public const string IsStatusRequired = "状态必须选择";

        public const string IsIntegerRequired = "请输入一个有效的整数";

        public const string IsMobileRequired = "请输入一个有效的电话号码";

        public const string IsContentRequired = "内容不能为空";

        public const string IsFirstWordRequired = "拼音首字母不能为空";

        public const string IsProvinceRequired = "省不能为空";

        public const string IsCityRequired = "市不能为空";

        public const string IsRegioRequired = "地区不能为空";

        public const string IsMobilePatternError = "手机号码格式不正确,请输入11位有效手机号码";

        public const string IsTelPatternError = "电话号码格式不正确，请输入如:028-XXXXXXXX";

        public const string IsEmailPatternError = "邮箱格式不正确";

        public const string IsZipCodePatternError = "请输入6位正确邮政编码";

        public const string IsAddressLengthError = "地址字符数范围为1-199";

        public const string IsRequired = "{0}不能为空";

        public const string IsBillTimeValid = "{0}不能超过当前日期";

        public const string IsGreaterZero = "{0}须大于零";

        public const string IsGreaterEqualZero = "{0}须大于等于零";

        public const string IsRepeat = "{0}已重复";

        public const string IsRepeated = "已重复";

        public const string IsPositiveNumber = "{0}必须是大于零的正整数";

        public const string IsDecimalNumber = "{0}必须是数字";

        public const string IsLengthError = "输入字符不能超过{0}个";
        public const string IsLengthGreater = "输入字符至少{0}个";

        public const string IsStringLengthError = "输入字符不能超过{1}个";

        public const string IsCodePatternError = "{0}格式不正确，只能输入字母和数字";

        public const string IsBankPatternError = "银行卡号格式不正确，只能输入数字16位或19位";

        public const string IsServerPatternError = "{0}格式不正确，只能输入字母、数字、点";

        public const string IsPwdPatternError = "{0}格式不正确，只能输入字母、数字、特殊字符（! @ # $ % ?等）";

        public const string IsInUse = "{0}正在被使用";
        public const string IsConfirmPasswordCorrect = "确认密码和密码不一致";

        public const string IsNotEqual = "{0}与{1}不一致";
        public const string PasswordError = "密码错误";

        public const string AccountNotExists = "该用户不存在";

        public const string InvoiceRepeated = "发票已被认领，请勿重复认领";

        public const string InvoiceNotExist = "发票不存在";

        public const string PublishStatus = "发布状态不允许操作";
    }
}

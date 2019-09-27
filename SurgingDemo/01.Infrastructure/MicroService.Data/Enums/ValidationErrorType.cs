using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MicroService.Data.Enums
{
    public enum ValidationErrorType
    {
        [Description("Body")]
        Body = 1,

        [Description("Items")]
        Items = 2,

        [Description("Accounts")]
        Accounts = 3,
    }
}

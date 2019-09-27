using System;

namespace MicroService.Core.Web.Filter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NoAuthTokenAttribute : Attribute
    {
    }
}

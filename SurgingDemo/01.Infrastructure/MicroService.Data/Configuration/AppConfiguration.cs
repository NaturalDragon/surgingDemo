using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Configuration
{
    public class AppConfiguration : LocalConfiguration
    {

        

        public override T GetValue<T>(string key)
        {
            if (!base.IsRemote)
                return base.GetValue<T>(key);

            return CloudRegistry.Configuration.GetValue<T>(key);
        }



    }
}

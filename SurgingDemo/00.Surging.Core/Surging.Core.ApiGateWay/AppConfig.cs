using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Surging.Core.ApiGateWay.Configurations;
using Surging.Core.CPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Core.ApiGateWay
{
    public static class AppConfig
    {
        public static IConfigurationRoot Configuration { get; set; }


        private static string _authorizationServiceKey;
        public static string AuthorizationServiceKey
        {
            get
            {
                return Configuration["AuthorizationServiceKey"] ?? _authorizationServiceKey;
            }
             set
            {

                _authorizationServiceKey = value;
            }
        }

        private static string _authorizationRoutePath;
        public static string AuthorizationRoutePath
        {
            get
            {
                return Configuration["AuthorizationRoutePath"] ?? _authorizationRoutePath;
            }
             set
            {

                _authorizationRoutePath = value;
            }
        }

        private static string _appSecretRoutePath;
        public static string AppSecretRoutePath
        {
            get
            {
                return Configuration["AppSecretRoutePath"] ?? _appSecretRoutePath;
            }
             set
            {

                _appSecretRoutePath = value;
            }
        }
        private static string _appSecretServiceKey;
        public static string AppSecretServiceKey
        {
            get
            {
                return Configuration["AppSecretServiceKey"] ?? _appSecretServiceKey;
            }
             set
            {

                _appSecretServiceKey = value;
            }
        }

        private static TimeSpan _appSecretTokenExpireTimeSpan = TimeSpan.FromMinutes(30);
        public static TimeSpan AppSercretTokenExpireTimeSpan
        {
            get
            {
                int tokenExpireTime;
                if (Configuration["AppSecretTokenExpireTimeSpan"] != null
                    && int.TryParse(Configuration["AppSecretTokenExpireTimeSpan"], out tokenExpireTime))
                {
                    _appSecretTokenExpireTimeSpan = TimeSpan.FromMinutes(tokenExpireTime);
                }
                return _appSecretTokenExpireTimeSpan;
            }
             set
            {
                _appSecretTokenExpireTimeSpan = value;
            }
        }

        private static TimeSpan _accessTokenExpireTimeSpan = TimeSpan.FromMinutes(30);
        public static TimeSpan AccessTokenExpireTimeSpan
        {
            get
            {
                int tokenExpireTime;
                if (Configuration["AccessTokenExpireTimeSpan"] != null && int.TryParse(Configuration["AccessTokenExpireTimeSpan"], out tokenExpireTime))
                {
                    _accessTokenExpireTimeSpan = TimeSpan.FromMinutes(tokenExpireTime);
                }
                return _accessTokenExpireTimeSpan;
            }
             set
            {
                _accessTokenExpireTimeSpan = value;
            }
        }

        private static string _tokenEndpointPath = "oauth2/token";

        public static string TokenEndpointPath
        {
            get
            {
                return Configuration["TokenEndpointPath"] ?? _tokenEndpointPath;
            }
             set
            {
                _tokenEndpointPath = value;
            }
        }

        public static Register Register
        {
            get
            {
                var result = new Register();
                var section = Configuration.GetSection("Register");
                if (section != null)
                    result = section.Get<Register>();
                return result;
            }

        }

        public static ServicePart ServicePart
        {
            get
            {
                var result = new ServicePart();
                var section = Configuration.GetSection("ServicePart");
                if (section != null)
                    result = section.Get<ServicePart>();
                return result;
            }
        }

        public static AccessPolicy Policy
        {
            get
            {
                var result = new AccessPolicy();
                var section = Configuration.GetSection("AccessPolicy");
                if (section != null)
                    result = section.Get<AccessPolicy>();
                return result;
            }
        }

        private static string _cacheMode = "MemoryCache";

        public static string CacheMode
        {
            get
            {
                if (Configuration == null)
                    return _cacheMode;
                return Configuration["CacheMode"] ?? _cacheMode;
            }
            set
            {
                _cacheMode = value;
            }

        }
    }
}

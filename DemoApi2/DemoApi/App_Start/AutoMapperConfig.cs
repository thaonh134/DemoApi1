using AutoMapper;
using DemoApi.AutoMapConfig.UserMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.App_Start
{
    public static class AutoMapperConfig
    {
        [Obsolete]
        public static IConfigurationProvider BuildConfiguration()
        {
            var assemblies = typeof(UserMapping).Assembly;
            Mapper.Initialize(cfg => cfg.AddProfiles(assemblies));
            return Mapper.Configuration;
        }

    }
}
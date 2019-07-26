using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DemoApi.Common.AppSettingWebConfig
{
    public class ProductionSetting : BaseAppSettingWebConfig, IAppSettingWebConfig
    {
        public override string rootResourcePath => ConfigurationManager.AppSettings.Get("RootResourcePathProduction");
        public override string rootAvatarPath => ConfigurationManager.AppSettings.Get("RootAvatarPathProduction");
    }
}

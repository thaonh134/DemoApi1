using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.AppSettingWebConfig
{
    public class StagingSetting : BaseAppSettingWebConfig, IAppSettingWebConfig
    {
        public override string rootResourcePath => ConfigurationManager.AppSettings.Get("RootResourcePathStaging");
        public override string rootAvatarPath => ConfigurationManager.AppSettings.Get("RootAvatarPathStaging");
    }
}

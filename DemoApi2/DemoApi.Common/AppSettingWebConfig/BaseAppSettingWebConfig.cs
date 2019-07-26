using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.AppSettingWebConfig
{
    public class BaseAppSettingWebConfig : IAppSettingWebConfig
    {
        public virtual string rootResourcePath => ConfigurationManager.AppSettings.Get("RootResourcePathStaging");
        public virtual string rootTempPath => ConfigurationManager.AppSettings.Get("TempUpload");
        public virtual string rootAvatarPath => ConfigurationManager.AppSettings.Get("RootAvatarPathStaging");
    }
}

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.AppSettingWebConfig
{
    public class WindsorInstall : IWindsorInstaller
    {
        static readonly bool isProduction = !string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("IsProduction")) && ConfigurationManager.AppSettings.Get("IsProduction").ToLowerInvariant() == "true";
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (isProduction)
                container.Register(Component.For<IAppSettingWebConfig>().ImplementedBy<ProductionSetting>().LifestylePerWebRequest());
            else
                container.Register(Component.For<IAppSettingWebConfig>().ImplementedBy<StagingSetting>().LifestylePerWebRequest());
        }
    }
}

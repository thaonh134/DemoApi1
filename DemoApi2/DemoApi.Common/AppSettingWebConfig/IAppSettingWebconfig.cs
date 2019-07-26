using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.AppSettingWebConfig
{
    public interface IAppSettingWebConfig
    {
        string rootAvatarPath { get; }
        string rootResourcePath { get; }
        string rootTempPath { get; }
    }
}

using DemoApi.Common.AppSettingWebConfig;
using DemoApi.Common.FileManager;
using DemoApi.HttpActionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DemoApi.Controllers.V1
{
    [AllowAnonymous]
    [RoutePrefix("api/v1/FileManager")]
    public class FileManagerController : ApiController
    {
        private IFileManager _fileManager;
        private IAppSettingWebConfig _appSettingWebConfig;

        public FileManagerController(IFileManager fileManager, IAppSettingWebConfig appSettingWebConfig)
        {
            _fileManager = fileManager;
            _appSettingWebConfig = appSettingWebConfig;
        }

        [Route("uploadfileToTempFolder")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadFileToTempFolder(HttpRequestMessage requestMessage)
        {
            string resourceUrl = await _fileManager.UploadFileStreamAsync(_appSettingWebConfig.rootResourcePath);
            return new TCSuccessHttpActionResult(requestMessage, resourceUrl);
        }
    }
}

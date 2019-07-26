using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DemoApi.Common.FileManager;
//using DemoApi.Common.VOVConstants;
using DemoApi.Common.Exceptions;
using DemoApi.Common.Helper;

namespace DemoApi.Common.FileManager
{
    public class FileManager : IFileManager
    {
        public string DownloadFileFromUrl(string url, string rootPath, string fileName)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    var newFileName = GUIDHelper.GenerateGuid() + "_" + fileName;
                    var pathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + rootPath), newFileName);
                    webClient.DownloadFile(url, pathFile);
                    return rootPath + "/" + newFileName;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private List<FileInfoModel> GetAllFileAndFolderFromPath(List<string> fileTypes, string path, string search, bool isOrderExtension, bool isExpanded)
        {
            var result = new List<FileInfoModel>();

            if (!Directory.Exists(path))
                return result;

            var searchByName = "*";
            var searchPatterns = new List<string>();
            if (!string.IsNullOrEmpty(search))
            {
                searchByName = string.Format("*{0}*", search);
            }

            if (fileTypes.Any() && !isOrderExtension)
            {
                fileTypes.ForEach(x => searchPatterns.Add(string.Format("{0}{1}", searchByName, x)));
            }
            else
            {
                searchPatterns.Add(string.Format("{0}.*", searchByName));
            }

            if (isExpanded)
            {
                GetFiles(result, isOrderExtension ? fileTypes : null, searchPatterns, path, SearchOption.AllDirectories);
            }
            else
            {
                var dirs = Directory.GetDirectories(path, searchByName, SearchOption.TopDirectoryOnly);
                foreach (var dir in dirs)
                {
                    var info = new DirectoryInfo(dir);
                    result.Add(GetFileInfo(info, 0, true));
                }
                GetFiles(result, isOrderExtension ? fileTypes : null, searchPatterns, path, SearchOption.TopDirectoryOnly);
            }

            return result;
        }

        private FileInfoModel GetFileInfo(FileSystemInfo info, long lenth, bool isFolder)
        {
            return new FileInfoModel
            {
                Name = info.Name,
                FullName = info.FullName,
                DateModified = info.LastWriteTime,
                IsFolder = isFolder,
                Length = lenth
            };
        }

        private void GetFiles(List<FileInfoModel> result, List<string> ignoreFileTypes, List<string> searchPatterns, string path, SearchOption searchOption)
        {
            searchPatterns.ForEach(searchPattern =>
            {
                var query = Directory.GetFiles(path, searchPattern, searchOption).AsQueryable();

                if (ignoreFileTypes != null)
                    ignoreFileTypes.ForEach(ignore => {
                        query = query.Where(x => !x.EndsWith(ignore));
                    });

                var files = query.ToList();
                files.ForEach(file =>
                {
                    var info = new System.IO.FileInfo(file);
                    result.Add(GetFileInfo(info, info.Length, false));
                });
            });
        }

        private async Task ReadAndWriteStreamBuffer(HttpContext context, string filePath, int bufferLength)
        {
            char[] buffer = new char[bufferLength];
            using (var reader = new StreamReader(context.Request.GetBufferlessInputStream(true)))
            using (var filestream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, true))
            using (var writer = new StreamWriter(filestream))
            {
                var readBuffer = await reader.ReadAsync(buffer, 0, buffer.Length);
                await writer.WriteAsync(buffer, 0, readBuffer);
            }
        }

        /// <summary>
        /// read file stream
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<byte[]> ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    await ms.WriteAsync(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// write file stream
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        private async Task WriteStreamAsync(byte[] ms, string pathFile)
        {
            using (FileStream filestream = new FileStream(pathFile, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                await filestream.WriteAsync(ms, 0, ms.Length);
            }
        }

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public FileMemoryStreamModel GetFileStream(string filePath)
        {
            var info = new FileInfo(filePath);
            var file = File.ReadAllBytes(filePath);
            var result = new FileMemoryStreamModel
            {
                FileName = info.Name,
                MemoryStream = new MemoryStream(file)
            };
            return result;
        }

        public string UploadFile(string rootPath)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                var file = HttpContext.Current.Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = GUIDHelper.GenerateGuid() + "_" + Path.GetFileName(file.FileName);
                    var pathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + rootPath), fileName);
                    file.SaveAs(pathFile);
                    return rootPath + fileName;
                }

                return null;
            }
            return null;
        }

        public async Task<string> UploadFileStreamAsync(string rootPath)
        {
            var files = HttpContext.Current.Request.Files;
            var result = new List<string>();
            for (var i = 0; i<files.Count;i++)
            {
                var file = files[i];
                var fileName = GUIDHelper.GenerateGuid() + "_" + Path.GetFileName(file.FileName);
                var pathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + rootPath), fileName);

                //using (Stream stream = HttpContext.Current.Request.GetBufferlessInputStream(true))
                using (Stream stream = file.InputStream)
                using (MemoryStream ms = new MemoryStream())
                using (FileStream filestream = new FileStream(pathFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    byte[] buffer = new byte[stream.Length];
                    int read;
                    while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await ms.WriteAsync(buffer, 0, read);
                    }
                    await filestream.WriteAsync(buffer, 0, buffer.Length);
                }
                result.Add( rootPath + fileName);

            }
            return String.Join(";", result.ToArray()); 
        }

        public IEnumerable<FileInfoModel> FilterFileInfo(List<string> fileTypes, DateTime? dateFrom, DateTime? dateTo, string path, string keyword, bool isOrderExtension, bool isExpanded)
        {
            var result = GetAllFileAndFolderFromPath(fileTypes, path, keyword, isOrderExtension, isExpanded).Where(x => (dateFrom == null || x.DateModified.Date >= dateFrom.Value)
                && (dateTo == null || x.DateModified.Date <= dateTo.Value));

            return result;
        }

        public async Task<string> MoveFileAsyn(string pathUrl, string newPathUrl)
        {
            var pathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + pathUrl));
            var newPathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + newPathUrl));
            if (Exists(pathFile))
            {
                if (pathFile == newPathFile) return newPathUrl;
                using (FileStream dst = new FileStream(newPathFile, FileMode.Create, FileAccess.Write, FileShare.Write))
                using (FileStream src = new FileStream(pathFile, FileMode.Open))
                {
                    await src.CopyToAsync(dst);
                }
                System.IO.File.Delete(pathFile);
                return newPathUrl;
            }
            else
            {
                throw new BaseApiException("File_Not_Existed", "File_Not_Existed");
            }
        }

        public string ReplacePath(string oldPath, string oldValue, string newValue)
        {
            //oldValue = oldValue.Replace("/", "\\");
            //oldValue = oldValue[0] != '\\' ? ("\\" + oldValue) : oldValue;
            //newValue = newValue.Replace("/", "\\");
            //newValue = newValue[0] != '\\' ? ("\\" + newValue) : newValue;
            oldPath = oldPath.Replace(oldValue, newValue);
            return oldPath;
        }

        public void RemoveFile(string pathUrl)
        {
            var pathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + pathUrl));
            if (Exists(pathFile))
            {
                System.IO.File.Delete(pathFile);
            }
            else
            {
                throw new BaseApiException("File_Not_Existed", "File_Not_Existed");
            }
        }

        public async Task<string> UploadFileStreamAsync(string rootPath, FileStreamModel fileStreamModel)
        {
            string fileName;
            fileName = GUIDHelper.GenerateGuid() + "_" + fileStreamModel.FileName;
            var pathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + rootPath), fileName);

            using (var fileStream = new FileStream(pathFile, FileMode.Create, FileAccess.Write))
            {
                fileStreamModel.FileStream.Position = 0;
                await fileStreamModel.FileStream.CopyToAsync(fileStream);
            }

            return rootPath + "/" + fileName;
        }

        public async Task<string> UploadFilesStreamAsync(string rootPath, List<FileStreamModel> filesStreamModel)
        {
            if (filesStreamModel.Count() == 0)
                return string.Empty;

            List<string> result = new List<string>();
            foreach(var fileStream in filesStreamModel)
            {
                string path = await UploadFileStreamAsync(rootPath, fileStream);
                result.Add(path);
            }
            return String.Join(";", result.ToArray());
        }
    }
}

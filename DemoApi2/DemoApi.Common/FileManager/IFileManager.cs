using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApi.Common.FileManager;

namespace DemoApi.Common.FileManager
{
    public interface IFileManager
    {
        string DownloadFileFromUrl(string url, string rootPath, string fileName);
        bool Exists(string filePath);
        FileMemoryStreamModel GetFileStream(string filePath);
        IEnumerable<FileInfoModel> FilterFileInfo(List<string> fileTypes, DateTime? dateFrom, DateTime? dateTo, string path, string keyword, bool isOrderExtension, bool isExpanded);
        Task<string> UploadFileStreamAsync(string rootPath);
        Task<string> UploadFileStreamAsync(string rootPath, FileStreamModel fileStreamModel);
        Task<string> UploadFilesStreamAsync(string rootPath, List<FileStreamModel> filesStreamModel);
        string UploadFile(string rootPath);
        Task<string> MoveFileAsyn(string pathUrl, string newPathUrl);
        string ReplacePath(string oldPath, string oldValue, string newValue);
        void RemoveFile(string pathUrl);
    }
}

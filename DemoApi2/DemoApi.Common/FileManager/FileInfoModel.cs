using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.FileManager
{
    public class FileInfoModel
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long Length { get; set; }
        public DateTime DateModified { get; set; }
        public string Extension
        {
            get
            {
                if (!string.IsNullOrEmpty(Name) && Name != FileConstant.FileNotesAndAttachments)
                {
                    var info = new System.IO.FileInfo(Name);
                    return info?.Extension ?? "";
                }
                return "";
            }
        }
        public string Folder { get; set; }
        public bool IsFolder { get; set; }
        public bool IsNote { get; set; }
        public void SetFolderSize()
        {
            if (IsFolder)
            {
                this.Length = 0;
                var files = Directory.GetFiles(this.FullName, "*.*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var info = new System.IO.FileInfo(file);
                    this.Length += info.Length;
                }
            }
        }
        public void SetParentFolder()
        {
            if (!IsFolder && !string.IsNullOrEmpty(this.FullName))
            {
                var info = Directory.GetParent(this.FullName);
                this.Folder = info?.Name;
            }
        }

        public static string MapFullUrl(string embeddedPath, string folderPath, Guid? guid, string fileName)
        {
            //NinjectContainerManager.Get<T>()
            var guiIdStr = guid?.ToString();
            if (!string.IsNullOrEmpty(guiIdStr) && !string.IsNullOrEmpty(fileName))
            {
                var fileInfo = new System.IO.FileInfo(fileName);
                var subFolder = folderPath.PadLeft(9, '0');
                var fullUrl = string.Format("{0}\\{1}\\{2}\\{3}\\{4}{5}"
                    , embeddedPath
                    , subFolder.Substring(0, 3)
                    , subFolder.Substring(3, 3)
                    , subFolder.Substring(6, 3)
                    , guiIdStr
                    , FileConstant.DAT);
                return fullUrl;
            }
            return "";
        }
    }
}

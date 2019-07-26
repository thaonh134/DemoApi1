using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.FileManager
{
    public class FileStreamModel
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string Extention { get; set; }
    }
}

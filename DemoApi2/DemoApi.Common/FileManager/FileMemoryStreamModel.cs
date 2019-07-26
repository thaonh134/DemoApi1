using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.FileManager
{
    public class FileMemoryStreamModel
    {
        public string FileName { get; set; }
        public MemoryStream MemoryStream { get; set; }
    }
}

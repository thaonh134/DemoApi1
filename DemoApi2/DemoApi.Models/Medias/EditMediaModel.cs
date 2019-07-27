using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Medias
{
    public class EditMediaModel
    {
        public EditMediaModel()
        {
            LastModificationTime = DateTime.Now;
        }
        public int UserId { get; set; }
        public string Type { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int IsDelete { get; set; }
    }
}

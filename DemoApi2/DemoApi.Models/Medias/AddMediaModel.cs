using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.Medias
{
    public class AddMediaModel
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IsDelete { get; set; }
    }
}

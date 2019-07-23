using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ReadOnlyDatabaseAttribute : Attribute
    {
        public ReadOnlyDatabaseAttribute()
        {
        }
    }
}

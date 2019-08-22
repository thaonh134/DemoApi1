using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Enums
{
    public enum GenderType
    {
        [Description(GenderDefineName.Now_know)]
        Not_know = 1,
        [Description(GenderDefineName.Male)]
        Male = 2,
        [Description(GenderDefineName.Female)]
        Female = 3
    }

    public static class GenderDefineName
    {
        public const string Now_know = "Not know";
        public const string Male = "Male";
        public const string Female = "Female";
    }
}

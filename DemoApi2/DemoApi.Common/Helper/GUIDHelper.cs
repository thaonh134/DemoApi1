using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Helper
{
    public static class GUIDHelper
    {
        private static Random random = new Random();
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateForRouteStepDecode()
        {
            string randomChar = RandomString(10);
            return string.Format("{0}_{1}", GenerateGuid(), randomChar);
        }
    }
}

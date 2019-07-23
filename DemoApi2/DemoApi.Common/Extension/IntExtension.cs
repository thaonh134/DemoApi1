using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Extension
{
    public static class IntExtensions
    {
        public static int GetBoundedValue(this int value, int min, int max)
        {
            var boundedValue = Math.Min(Math.Max(value, min), max);
            return boundedValue;
        }
        public static int GetBoundedValue(this int value, int min)
        {
            var boundedValue = Math.Max(value, min);
            return boundedValue;
        }
        public static int DefaultZeroIfNull(this int? value)
        {
            return value.HasValue ? value.Value : 0;
        }
        public static int GetBoundedValue(this int? value, int defaultValue, int min)
        {
            var valToBound = value ?? defaultValue;
            var boundedValue = Math.Max(valToBound, min);
            return boundedValue;
        }
        public static int GetBoundedValue(this int? value, int defaultValue, int min, int max)
        {
            var valToBound = value ?? defaultValue;
            var boundedValue = GetBoundedValue(valToBound, min, max);
            return boundedValue;
        }
    }
}

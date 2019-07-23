using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Pagination
{
    public static class PaginationConst
    {
        public const string PageNumberName = "pageNumber";
        public const string PageSizeName = "pageSize";
        public const string TotalItemCountName = "totalItemCount";
        public const string ValueLessThanOneErrorMessage = "Value may not be less than 1.";
        public const string ValueLessThanZeroErrorMessage = "Value may not be less than 0.";

        public const int MinPageSize = 1;
        public const int MinPageNumber = 1;
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 20;
        public const int MaximumPageNumber = 20;
        public const int MaxPageSize = 200;
        public const int DefaultConversationPageSize = 200;
        public const int MaxConversationPageSize = 1000;
    }
}

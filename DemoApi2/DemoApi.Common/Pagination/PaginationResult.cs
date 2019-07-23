using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Pagination
{
    public class PaginationResult
    {
        public PaginationResult()
        {
        }

        public PaginationResult(PaginationRequest pageDataRequest, int totalItemCount)
        {
            if (pageDataRequest.PageNumber != -1)
            {
                TotalPageCount = CalculatePageCount(totalItemCount, pageDataRequest.PageSize);
                TotalItemCount = totalItemCount;
                PageSize = pageDataRequest.PageSize;
                PageNumber = pageDataRequest.PageNumber;
            }
            else
            {
                // Get all record from data base
                TotalPageCount = -1;
                TotalItemCount = -1;
                PageSize = -1;
                PageNumber = -1;
            }
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        private int CalculatePageCount(int totalItemCount, int pageSize)
        {
            if (totalItemCount < 0)
                throw new ArgumentOutOfRangeException(PaginationConst.TotalItemCountName,
                    totalItemCount, PaginationConst.ValueLessThanZeroErrorMessage);
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(PaginationConst.PageSizeName,
                    pageSize, PaginationConst.ValueLessThanOneErrorMessage);
            var totalPageCount = (totalItemCount + pageSize - 1) / pageSize;
            return totalPageCount;
        }
    }
}

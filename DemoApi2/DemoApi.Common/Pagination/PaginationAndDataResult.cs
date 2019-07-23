using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Pagination
{
    public class PaginationAndDataResult<T>
    {
        public PaginationAndDataResult()
        {
        }

        public PaginationAndDataResult(List<T> items, PaginationRequest pageDataRequest, int totalItemCount)
        {
            QueriedItems = items;
            PaginationResult = new PaginationResult(pageDataRequest, totalItemCount);
        }

        public PaginationResult PaginationResult { get; set; }
        public List<T> QueriedItems { get; set; }
    }
}

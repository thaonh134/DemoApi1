using DemoApi.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Common.Pagination
{
    public class PaginationRequest
    {
        private int _pageNumber;
        private int _pageSize;
        private string _locale;
        private int _startIndex;
        private DateTimeOffset _lastTime;

        public PaginationRequest()
        {
        }

        public PaginationRequest(int pageNumber)
        {
            PageNumber = pageNumber;
            PageSize = PaginationConst.DefaultPageSize;
        }

        public PaginationRequest(int pageNumber, int? pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize.HasValue ? pageSize.Value : PaginationConst.DefaultPageSize;
            //Locale = locale;
        }

        public PaginationRequest(int pageNumber, int pageSize, string locale, DateTimeOffset lastTime)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Locale = locale;
            LastTime = lastTime;
        }

        public PaginationRequest(int pageNumber, int pageSize, DateTimeOffset lastTime, string locale)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Locale = locale;
            LastTime = lastTime;
        }

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                if (value == -1)
                {
                    _pageNumber = value;
                }
                else
                {
                    _pageNumber = value.GetBoundedValue(PaginationConst.MinPageNumber);
                }
            }
        }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value.GetBoundedValue(PaginationConst.MinPageSize, PaginationConst.MaxPageSize);
            }
        }
        public string Locale
        {
            get
            {
                return _locale;
            }
            set
            {
                _locale = value;
            }
        }
        public DateTimeOffset LastTime
        {
            get
            {
                return _lastTime;
            }
            set
            {
                _lastTime = value;
            }
        }

        public int StartIndex
        {
            get
            {
                if (_startIndex == 0)
                    _startIndex = CalculateStartIndex(_pageNumber, _pageSize);

                return _startIndex;
            }
        }

        private static int CalculatePageSize(int requestedValue, int maxValue)
        {
            if (requestedValue < 1)
                throw new ArgumentOutOfRangeException(
                    "requestedValue", requestedValue, PaginationConst.ValueLessThanOneErrorMessage);
            if (maxValue < 1)
                throw new ArgumentOutOfRangeException(
                    "maxValue", maxValue, PaginationConst.ValueLessThanOneErrorMessage);
            var boundedPageSize = Math.Min(requestedValue, maxValue);
            return boundedPageSize;
        }

        private static int CalculateStartIndex(int pageNumber, int pageSize)
        {
            if (pageNumber == -1)
            {
                return -1;
            }
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(
                    PaginationConst.PageNumberName, pageNumber,
                    PaginationConst.ValueLessThanOneErrorMessage);
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(
                    PaginationConst.PageSizeName, pageSize,
                    PaginationConst.ValueLessThanOneErrorMessage);
            var startIndex = (pageNumber - 1) * pageSize;
            return startIndex;
        }


    }
}

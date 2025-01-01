using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pagination
{
    public class PaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public string? Sort { get; set; }
        private int _pageSize = 10;
        private string? _search;
        protected const int MaxPageSize = 20;

        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}

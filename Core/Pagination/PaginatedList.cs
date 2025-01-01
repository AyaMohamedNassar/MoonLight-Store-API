using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pagination
{
    public class PaginatedList<T> where T : class
    {
  
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItems {  get; private set; }
        public IReadOnlyList<T> Items { get; private set; }

        public PaginatedList(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalItems)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalItems / (double) pageSize);
            PageSize = pageSize;
            Items = items;
            TotalItems = totalItems;
        }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        
    }
}

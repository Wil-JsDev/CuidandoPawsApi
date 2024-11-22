using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Domain.Pagination
{
    // Change this a Interfaces
    public class PagedResult <T> 
    {
        public PagedResult(IEnumerable<T>? items, int totalItems, int currentPage, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalItems/(double)pageSize);
        }

        public IEnumerable<T>? Items { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

    }
}

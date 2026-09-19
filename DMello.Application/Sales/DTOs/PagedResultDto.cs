using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.Sales.DTOs
{
    //Sends total record count and page counts to Angular so our UI can render page buttons (Prev, Next, 1, 2, 3).
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}

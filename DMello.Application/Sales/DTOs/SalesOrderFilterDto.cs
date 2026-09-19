using System;
using System.Collections.Generic;
using System.Text;

namespace DMello.Application.Sales.DTOs
{
    //Why it exists: Captures incoming search filters and page parameters from Angular query parameters.
    public class SalesOrderFilterDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderNo { get; set; }
        public DateTime? Date { get; set; }
    }
}

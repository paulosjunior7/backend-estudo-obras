namespace Obras.Business.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PaginationDetails
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
    }
}

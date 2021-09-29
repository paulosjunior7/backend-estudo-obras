namespace Obras.Business.SharedDomain.Models
{
    using System.Collections.Generic;

    public class PageResponse<T>
    {
        public List<T> Nodes { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}

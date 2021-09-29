namespace Obras.Business.SharedDomain.Models
{

    public class PaginationDetails
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
    }
}

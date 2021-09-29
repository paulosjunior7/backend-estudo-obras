using Obras.Business.SharedDomain.Enums;

namespace Obras.Business.SharedDomain.Models
{
    public class SortingDetails<T>
    {
        public T Field { get; set; }
        public SortingDirection Direction { get; set; }
    }
}

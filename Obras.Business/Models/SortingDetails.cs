using Obras.Business.Enums;

namespace Obras.Business.Models
{
    public class SortingDetails<T>
    {
        public T Field { get; set; }
        public SortingDirection Direction { get; set; }
    }
}

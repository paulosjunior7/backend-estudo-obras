using Obras.Business.Enums;

namespace Obras.Business.Models
{
    public class PageRequest<T, E>
    {
        public int? First { get; set; }
        public int? Last { get; set; }
        public int? Size { get; set; }
        public string After { get; set; }
        public string Before { get; set; }
        public T Filter { get; set; }
        public SortingDetails<E> OrderBy { get; set; }
    }
}

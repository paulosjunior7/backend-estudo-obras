using Obras.Data.Enums;

namespace Obras.Business.PhotoDomain.Models
{
    public class PhotoFilter
    {
        public int? Id { get; set; }
        public int? ConstructionId { get; set; }
        public TypePhoto? TypePhoto { get; set; }
    }
}

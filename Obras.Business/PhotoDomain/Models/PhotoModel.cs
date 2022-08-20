using Obras.Data.Enums;
using System.IO.Abstractions;

namespace Obras.Business.PhotoDomain.Models
{
    public class PhotoModel
    {
        public IFile File { get; set; }
        public TypePhoto TypePhoto { get; set; }
        public int ConstrucationId { get; set; }
        public string RegistrationUserId { get; set; }
    }
}

using Obras.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public TypePhoto TypePhoto{ get; set; }
        public virtual Construction Construction { get; set; }
        public int ConstrucationId { get; set; }
        public string RegistrationUserId { get; set; }
        public virtual User RegistrationUser { get; set; }
        public string ChangeUserId { get; set; }
        public virtual User ChangeUser { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }

    }
}

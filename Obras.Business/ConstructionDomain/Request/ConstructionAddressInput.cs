using Obras.Data.Enums;
using System;
namespace Obras.Business.ConstructionDomain.Request
{
    public class ConstructionAddressInput
    {
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? Number { get; set; }
        public string? Neighbourhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Complement { get; set; }
    }
}


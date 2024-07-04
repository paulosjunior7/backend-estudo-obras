using Obras.Data.Enums;
using System;
namespace Obras.Business.ConstructionDomain.Request
{
    public class ConstructionDateInput
    {
        public string? Identifier { get; set; }
        public StatusConstruction StatusConstruction { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}


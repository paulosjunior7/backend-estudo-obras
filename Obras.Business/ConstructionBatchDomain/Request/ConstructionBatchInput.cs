using System;
namespace Obras.Business.ConstructionBatchDomain.Request
{
    public class ConstructionBatchInput
    {
        public int PeopleId { get; set; }
        public double Value { get; set; }
        public bool Active { get; set; }
    }
}


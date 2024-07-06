
using Obras.Business.PeopleDomain.Models;
using System.Collections.Generic;

namespace Obras.Business.ConstructionInvestorDomain.Response
{
    public class ConstructionInvestorResponse
    {
        public int Id { get; set; }
        public int ConstructionId { get; set; }
        public int PeopleId { get; set; }
        public virtual PeopleModel People { get; set; }
        public bool Active { get; set; }

    }
}


using Obras.Business.PeopleDomain.Response;
using Obras.Data.Entities;
using System;
namespace Obras.Business.ConstructionBatchDomain.Response
{
    public class ConstructionBatchResponse
    {
        public int Id { get; set; }
        public int ConstructionId { get; set; }
        public int PeopleId { get; set; }
        public virtual PeopleResponse People { get; set; }
        public double Value { get; set; }
        public bool Active { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}


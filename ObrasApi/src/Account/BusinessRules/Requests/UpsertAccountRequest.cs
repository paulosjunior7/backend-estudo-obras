using System;

namespace ObrasApi.src.Account.BusinessRules.Requests
{
    public class UpsertAccountRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public Guid IdCompany { get; set; }
        public bool Active { get; set; }
    }
}
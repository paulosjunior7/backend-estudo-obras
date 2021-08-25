using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace ObrasApi.src.Account.BusinessRules.Response
{
    public class UpsertAccountResponse
    {
        public UpsertAccountResponsePayload Payload { get; set; }
        public List<ValidationFailure> Errors { get; set; }
    }
    public class UpsertAccountResponsePayload
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public Guid IdCompany { get; set; }
        public bool Active { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
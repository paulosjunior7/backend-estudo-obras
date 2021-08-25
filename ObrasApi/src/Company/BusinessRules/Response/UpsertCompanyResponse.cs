using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace ObrasApi.src.Company.BusinessRules.Response
{
    public class UpsertCompanyResponse
    {
        public UpsertCompanyResponsePayload Payload { get; set; }
        public List<ValidationFailure> Errors { get; set; }
    }
    public class UpsertCompanyResponsePayload
    {
        public Guid Id { get; set; }
        public string Cnpj { get; set; }
        public string CorporateName { get; set; }
        public string FantasyName { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
        public string Telephone { get; set; }
        public string CellPhone { get; set; }
        public string EMail { get; set; }
        public bool Active { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
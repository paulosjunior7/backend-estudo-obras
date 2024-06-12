namespace Obras.Business.EmployeeDomain.Models
{
    using System;

    public class EmployeeFilter
    {
        public int? Id { get; set; }
        public string? Cpf { get; set; }
        public string? Name { get; set; }
        public int? ResponsibilityId { get; set; }
        public string? Neighbourhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public bool? Active { get; set; }
        public int? CompanyId { get; set; }
    }
}

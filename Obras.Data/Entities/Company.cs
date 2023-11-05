using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obras.Data.Entities
{
    public class Company
    {
        #region Fields
        public int Id { get; set; }
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
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<Brand> Brands { get; set; }
        public ICollection<Construction> Constructions { get; set; }
        public ICollection<Documentation> Documentations { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Outsourced> Outsourceds { get; set; }
        public ICollection<People> Peoples { get; set; }

        #endregion

        #region Ctor
        public Company()
        {

        }
        #endregion
    }
}
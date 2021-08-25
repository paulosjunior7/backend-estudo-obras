using ObrasApi.src.Company.BusinessRules.Requests;
using ObrasApi.src.Company.BusinessRules.Response;
using ObrasApi.src.Company.Database.Repositories;
using System;

namespace ObrasApi.src.Company.BusinessRules.Handlers
{
    public class GetByIdCompanyHandler : IGetByIdCompanyHandler
    {
        private readonly ICompanyRepository _repository;

        public GetByIdCompanyHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public CompanyResponse Execute(GetByIdCompanyRequest request)
        {
            var company = _repository.GetById(request.Id);
            if (company == null)
                throw new Exception("Empresa não encontrada");

            return new CompanyResponse
            {
                Payload = new CompanyResponseItem
                {
                    Id = company.Id.Value,
                    EMail = company.EMail,
                    FantasyName = company.FantasyName,
                    Neighbourhood = company.Neighbourhood,
                    Number = company.Number,
                    State = company.State,
                    Telephone = company.Telephone,
                    ZipCode = company.ZipCode,
                    Active = company.Active,
                    Address = company.Address,
                    CellPhone = company.CellPhone,
                    City = company.City,
                    Cnpj = company.Cnpj,
                    Complement = company.Complement,
                    CorporateName = company.CorporateName,
                }
            };
        }
    }
}
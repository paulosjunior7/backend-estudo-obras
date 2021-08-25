using ObrasApi.src.Company.BusinessRules.Response;
using ObrasApi.src.Company.Database.Repositories;
using System.Linq;

namespace ObrasApi.src.Company.BusinessRules.Handlers
{
    public class GetAllCompaniesHandler : IGetAllCompaniesHandler
    {
        private readonly ICompanyRepository _repository;

        public GetAllCompaniesHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }
        public CompaniesResponse Execute()
        {
            var companies = _repository.GetAll()
                .Select(q => new CompanyResponseItem
                {
                    Id = q.Id.Value,
                    EMail = q.EMail,
                    FantasyName = q.FantasyName,
                    Neighbourhood = q.Neighbourhood,
                    Number = q.Number,
                    State = q.State,
                    Telephone = q.Telephone,
                    ZipCode = q.ZipCode,
                    Active = q.Active,
                    Address = q.Address,
                    CellPhone = q.CellPhone,
                    City = q.City,
                    Cnpj = q.Cnpj,
                    Complement = q.Complement,
                    CorporateName = q.CorporateName,
                })
                .ToList();
            return new CompaniesResponse
            {
                Payload = companies
            };
        }
    }
}
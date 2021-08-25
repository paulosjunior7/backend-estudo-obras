using ObrasApi.src.Company.BusinessRules.Requests;
using ObrasApi.src.Company.BusinessRules.Response;
using ObrasApi.src.Company.BusinessRules.Validators;
using ObrasApi.src.Company.Database.Domain;
using ObrasApi.src.Company.Database.Repositories;
using System;

namespace ObrasApi.src.Company.BusinessRules.Handlers
{
    public class UpsertCompanyHandler : IUpsertCompanyHandler
    {
        private readonly ICompanyRepository _repository;
        private readonly ICompanyValidator _validator;

        public UpsertCompanyHandler(ICompanyRepository repository, ICompanyValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public UpsertCompanyResponse Execute(UpsertCompanyRequest request)
        {
            var validatorResult = _validator.Validate(request);
            if (!validatorResult.IsValid)
            {
                return new UpsertCompanyResponse
                {
                    Errors = validatorResult.Errors
                };
            }

            CompanyDomain entity;

            if (request.Id.HasValue)
            {
                entity = _repository.GetById(request.Id.Value);
                if (entity == null)
                    throw new Exception("Empresa não encontrada");
            }
            else
            {
                entity = new CompanyDomain();
                entity.CreationDate = DateTime.Now;
            }

            entity.EMail = request.EMail;
            entity.FantasyName = request.FantasyName;
            entity.Neighbourhood = request.Neighbourhood;
            entity.Number = request.Number;
            entity.State = request.State;
            entity.Telephone = request.Telephone;
            entity.ZipCode = request.ZipCode;
            entity.Active = request.Active;
            entity.Address = request.Address;
            entity.CellPhone = request.CellPhone;
            entity.ChangeDate = DateTime.Now;
            entity.City = request.City;
            entity.Cnpj = request.Cnpj;
            entity.Complement = request.Complement;
            entity.CorporateName = request.CorporateName;

            _repository.Save(entity);

            return new UpsertCompanyResponse
            {
                Payload = new UpsertCompanyResponsePayload
                {
                    Id = entity.Id.Value,
                    EMail = entity.EMail,
                    FantasyName = entity.FantasyName,
                    Neighbourhood = entity.Neighbourhood,
                    Number = entity.Number,
                    State = entity.State,
                    Telephone = entity.Telephone,
                    ZipCode = entity.ZipCode,
                    Active = entity.Active,
                    Address = entity.Address,
                    CellPhone = entity.CellPhone,
                    City = entity.City,
                    Cnpj = entity.Cnpj,
                    Complement = entity.Complement,
                    CorporateName = entity.CorporateName,
                }
            };
        }
    }
}
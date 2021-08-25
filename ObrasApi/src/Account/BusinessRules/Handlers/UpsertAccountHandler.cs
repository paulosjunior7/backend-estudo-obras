using ObrasApi.src.Account.BusinessRules.Requests;
using ObrasApi.src.Account.BusinessRules.Response;
using ObrasApi.src.Account.BusinessRules.Validators;
using ObrasApi.src.Account.Database.Domain;
using ObrasApi.src.Account.Database.Repositories;
using System;

namespace ObrasApi.src.Account.BusinessRules.Handlers
{
    public class UpsertAccountHandler : IUpsertAccountHandler
    {
        private readonly IAccountRepository _repository;
        private readonly IAccountValidator _validator;

        public UpsertAccountHandler(IAccountRepository repository, IAccountValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public UpsertAccountResponse Execute(UpsertAccountRequest request)
        {
            var validatorResult = _validator.Validate(request);
            if (!validatorResult.IsValid)
            {
                return new UpsertAccountResponse
                {
                    Errors = validatorResult.Errors
                };
            }

            AccountDomain entity;

            if (request.Id.HasValue)
            {
                entity = _repository.GetById(request.Id.Value);
                if (entity == null)
                    throw new Exception("Conta não encontrada");
            }
            else
            {
                entity = new AccountDomain();
                entity.CreationDate = DateTime.Now;
            }

            entity.EMail = request.EMail;
            entity.Name = request.Name;
            entity.Password = request.Password;
            entity.IdCompany = request.IdCompany;
            entity.Active = request.Active;
            entity.ChangeDate = DateTime.Now;

            _repository.Save(entity);

            return new UpsertAccountResponse
            {
                Payload = new UpsertAccountResponsePayload
                {
                    Id = entity.Id.Value,
                    EMail = entity.EMail,
                    Name = entity.Name,
                    IdCompany = entity.IdCompany,
                    Active = entity.Active
                }
            };
        }
    }
}
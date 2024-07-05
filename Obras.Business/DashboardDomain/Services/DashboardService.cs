using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Obras.Business.DashboardDomain.Response;
using Obras.Data;
using Obras.Data.Entities;
using System.Threading.Tasks;

namespace Obras.Business.DashboardDomain.Services
{
    public interface IDashboardService
    {
        Task<TotalExpenseResponse> GetTotalExpense(int constructionId);
    }

    public class DashboardService : IDashboardService
    {
        private readonly ObrasDBContext _dbContext;
        private readonly IMapper _mapper;

        public DashboardService(ObrasDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TotalExpenseResponse> GetTotalExpense(int constructionId)
        {
            var response =  await _dbContext.Set<TotalExpense>()
                   .FromSqlRaw(@"
                         select c.Id,
                               (select SUM(Quantity * UnitPrice)
                                from ConstructionMaterials cm
                                where cm.ConstructionId = c.Id and cm.Active = 1) as Material,
                               (select SUM(Value)
                                from ConstructionManpowers cm
                                where cm.ConstructionId = c.Id and cm.Active = 1) as Equipe,
                               (select SUM(Value)
                                from ConstructionDocumentations cm
                                where cm.ConstructionId = c.Id and cm.Active = 1) as Documentacao,
                               (select SUM(Value)
                                from ConstructionExpenses cm
                                where cm.ConstructionId = c.Id and cm.Active = 1) as Despesa,
                               (select SUM(Value)
                                from ConstructionBatchs cm
                                where cm.ConstructionId = c.Id and cm.Active = 1) as ValorLote,
                               (select SUM(SaleValue)
                                from ConstructionHouses cm
                                where cm.ConstructionId = c.Id and cm.Active = 1) as ValorVenda
                        from Constructions c
                        where Id = {0}
                    ", constructionId)
                   .FirstOrDefaultAsync();

            return _mapper.Map<TotalExpenseResponse>(response);
        }
    }
}


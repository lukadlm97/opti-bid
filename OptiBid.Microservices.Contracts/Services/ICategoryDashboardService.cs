using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface ICategoryDashboardService
    {
        Task<OperationResult<EnumItem>> GetProducts(CancellationToken cancellationToken = default);
        Task<OperationResult<EnumItem>> GetServices(CancellationToken cancellationToken = default);
        Task<OperationResult<EnumItem>> GetCountries(CancellationToken cancellationToken);
        Task<OperationResult<EnumItem>> GetProfessions(CancellationToken cancellationToken);
        Task<OperationResult<EnumItem>> GetContactTypes(CancellationToken cancellationToken);
        Task<OperationResult<EnumItem>> GetUserRoles(CancellationToken cancellationToken);
    }
}

using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IAccountDashboardService
    {
        Task<OperationResult<EnumItem>> GetCountries(CancellationToken cancellationToken);
        Task<OperationResult<EnumItem>> GetProfessions(CancellationToken cancellationToken);
        Task<OperationResult<EnumItem>> GetContactTypes(CancellationToken cancellationToken);
        Task<OperationResult<EnumItem>> GetUserRoles(CancellationToken cancellationToken);
    }
}

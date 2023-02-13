using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.GrpcServices
{
    public interface IAccountEnumerationGrpcService
    {
        Task<IEnumerable<EnumItem>> GetCountries(CancellationToken cancellationToken = default);
        Task<IEnumerable<EnumItem>> GetContactTypes(CancellationToken cancellationToken = default);
        Task<IEnumerable<EnumItem>> GetProfessions(CancellationToken cancellationToken = default);
        Task<IEnumerable<EnumItem>> GetUserRoles(CancellationToken cancellationToken = default);
    }
}

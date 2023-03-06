using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output.Customer;

namespace OptiBid.Microservices.Contracts.GrpcServices
{
    public interface ICustomerGrpcService
    {
        Task<CustomerDetailsResult> GetCustomerDetails(CustomerRequest customerRequest,CancellationToken cancellationToken=default);
        Task<CustomerResult> Create(CustomerRequest customerRequest, CancellationToken cancellationToken = default);
        Task<IEnumerable<CustomerDetailsResult>> GetAll( CancellationToken cancellationToken = default);
    }
}

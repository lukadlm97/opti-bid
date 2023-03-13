
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface ICustomerService
    {
        Task<OperationResult<Domain.Output.Customer.CustomerResult>> OpenAccount(Domain.Input.CustomerRequest request,CancellationToken cancellationToken=default);
        Task<OperationResult<Domain.Output.Customer.CustomerDetailsResult>> Get(int id,CancellationToken cancellationToken=default);
        Task<OperationResult<Domain.Output.Customer.CustomerDetailsResult>> Get(PagingRequest pagingRequest,CancellationToken cancellationToken=default);
    }
}

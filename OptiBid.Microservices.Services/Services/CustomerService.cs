using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Contracts.Domain.Input;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.Domain.Output.Auction;
using OptiBid.Microservices.Contracts.Domain.Output.Customer;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.Utilities;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Infrastructure.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerGrpcService _customerGrpcService;
        private readonly IHybridCache<CustomerDetailsResult> _hybridCache;

        public CustomerService(ICustomerGrpcService customerGrpcService, IHybridCache<CustomerDetailsResult> hybridCache)
        {
            this._customerGrpcService = customerGrpcService;
            this._hybridCache = hybridCache;
        }
        public async Task<OperationResult<CustomerResult>> OpenAccount(CustomerRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _customerGrpcService.Create(new CustomerRequest(request.UserId, request.Username),
                cancellationToken: cancellationToken);
            if (result == null)
            {
                return new OperationResult<CustomerResult>(null, null, OperationResultStatus.BadRequest, null);
            }

            return new OperationResult<CustomerResult>(result, null, OperationResultStatus.Success, null);
        }

        public async Task<OperationResult<CustomerDetailsResult>> Get(int id, CancellationToken cancellationToken = default)
        {
            var key = nameof(CustomerDetailsResult) + id;
            CustomerDetailsResult result = await _hybridCache.Get(key, cancellationToken);
            if (result != null)
            {
                return new OperationResult<CustomerDetailsResult>(result, null, OperationResultStatus.Success, null);
            }

            result = await _customerGrpcService.GetCustomerDetails(new CustomerRequest(id, string.Empty),
                cancellationToken);
            if (result != null)
            {
                _ = _hybridCache.Set(key, result, cancellationToken);
                return new OperationResult<CustomerDetailsResult>(result, null, OperationResultStatus.Success, null);
            }

            return new OperationResult<CustomerDetailsResult>(null, null, OperationResultStatus.NotFound, null);
        }

        public async Task<OperationResult<IEnumerable<CustomerDetailsResult>>> Get(PagingRequest pagingRequest, CancellationToken cancellationToken = default)
        {
            var result = await _customerGrpcService.GetAll(cancellationToken);
            if (result == null)
            {
                return new OperationResult<IEnumerable<CustomerDetailsResult>>(null, null,
                    OperationResultStatus.NotFound, null);
            }

            return new OperationResult<IEnumerable<CustomerDetailsResult>>(result, null, OperationResultStatus.Success,
                null);
        }
    }
}

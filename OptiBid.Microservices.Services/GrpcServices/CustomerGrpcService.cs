using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Auction.Grpc.CustomersServiceDefinition;
using OptiBid.Microservices.Contracts.Domain.Output.Customer;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Services.Factory;
using CustomerRequest = OptiBid.Microservices.Contracts.Domain.Input.CustomerRequest;

namespace OptiBid.Microservices.Infrastructure.GrpcServices
{
    public class CustomerGrpcService:ICustomerGrpcService
    {
        private readonly ILogger<CustomerGrpcService> _logger;
        private readonly IAuctionGrpcFactory _auctionAssetsGrpcFactory;

        public CustomerGrpcService(ILogger<CustomerGrpcService> logger, IAuctionGrpcFactory auctionAssetsGrpcFactory)
        {
            _logger = logger;
            _auctionAssetsGrpcFactory = auctionAssetsGrpcFactory;
        }
        public async Task<CustomerDetailsResult> GetCustomerDetails(CustomerRequest customerRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionAssetsGrpcFactory.GetCustomerClient();
                var customerDetails = await grpcClient.GetAsync(new Auction.Grpc.CustomersServiceDefinition.CustomerRequest()
                {
                    UserId = customerRequest.UserId,
                    Username = customerRequest.Username
                },cancellationToken:cancellationToken);
                if (customerDetails.Status == OperationCompletionStatus.Success && customerDetails.Customer != null)
                {
                    return new CustomerDetailsResult(customerDetails.Customer.Id, customerDetails.Customer.UserId,
                        customerDetails.Customer.Username, customerDetails.Customer.DateOpened);
                }

                return null;
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex.Message);
                }
                return null;
            }
        }

        public async Task<CustomerResult> Create(CustomerRequest customerRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _auctionAssetsGrpcFactory.GetCustomerClient();
                var result = await client.CreateAsync(new Auction.Grpc.CustomersServiceDefinition.CustomerRequest()
                {
                    Username = customerRequest.Username,
                    UserId = customerRequest.UserId
                });

                if (result.Status == OperationCompletionStatus.Success)
                {
                    return new CustomerResult(result.CustomerId);
                }
                
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex.Message);
                }
            }

            return null;
        }

        public async Task<IEnumerable<CustomerDetailsResult>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _auctionAssetsGrpcFactory.GetCustomerClient();
                var customers = await client.GetAllAsync(new EmptyRequest(), cancellationToken: cancellationToken);
                if (customers.Status == OperationCompletionStatus.Success && customers.Customers != null)
                {
                    return customers.Customers.Select(x =>
                        new CustomerDetailsResult(x.Id, x.UserId, x.Username, x.DateOpened));
                }
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex.Message);
                }
            }

            return Array.Empty<CustomerDetailsResult>();
        }
    }
}

using AutoMapper;
using Grpc.Core;
using OptiBid.Microservices.Auction.Grpc.CustomersServiceDefinition;
using OptiBid.Microservices.Auction.Services.Enumerations;
using OptiBid.Microservices.Auction.Services.Services;

namespace OptiBid.Microservices.Auction.Grpc.Services
{
    public class CustomerService:Customer.CustomerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerService(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public override async Task<CustomerDetailsReplay> Get(CustomerRequest request, ServerCallContext context)
        {
            var customerResponse = await _customerService.Get(request.UserId, context.CancellationToken);
            return customerResponse.SearchStatus switch
            {
                SearchStatus.Success=>new CustomerDetailsReplay()
                {
                    Status = OperationCompletionStatus.Success,
                    Customer = _mapper.Map<CustomersServiceDefinition.CustomerDetails>(customerResponse.Customer)
                },
                SearchStatus.BadRequest=> new CustomerDetailsReplay()
                {
                    Status = OperationCompletionStatus.BadRequest
                },
                SearchStatus.NotFound=> new CustomerDetailsReplay()
                {
                    Status = OperationCompletionStatus.NotFound
                }
            };
        }

        public override async Task<CustomerCollection> GetAll(EmptyRequest request, ServerCallContext context)
        {
            var customerResponse = await _customerService.Get( context.CancellationToken);
            return customerResponse.SearchStatus switch
            {
                SearchStatus.Success => new CustomerCollection()
                {
                    Status = OperationCompletionStatus.Success,
                    Customers = { _mapper.Map<IEnumerable<CustomerDetails>>(customerResponse.Customers) }
                },
                SearchStatus.BadRequest => new CustomerCollection()
                {
                    Status = OperationCompletionStatus.BadRequest
                },
                SearchStatus.NotFound => new CustomerCollection()
                {
                    Status = OperationCompletionStatus.NotFound
                }
            };
        }

        public override async Task<CustomerReply> Create(CustomerRequest request, ServerCallContext context)
        {
            var response = await _customerService.Add(_mapper.Map<Domain.Input.Customer>(request));

            return response.CreationStatus switch
            {
                CreationStatus.Success => new CustomerReply()
                {
                    Status = OperationCompletionStatus.Success,
                    CustomerId = response.Customer.Id
                },
                CreationStatus.Unknown or CreationStatus.BadRequest or CreationStatus.Error => new CustomerReply()
                {
                    Status = OperationCompletionStatus.BadRequest
                },
                CreationStatus.Exist => new CustomerReply()
                {
                    Status = OperationCompletionStatus.BadRequest,
                    CustomerId = -1
                },
            };
        }
    }
}

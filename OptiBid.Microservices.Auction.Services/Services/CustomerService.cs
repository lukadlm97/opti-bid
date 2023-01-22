using AutoMapper;
using OptiBid.Microservices.Auction.Domain.Input;
using OptiBid.Microservices.Auction.Services.Enumerations;
using OptiBid.Microservices.Auction.Services.Models;
using OptiBid.Microservices.Auction.Services.UnitOfWork;

namespace OptiBid.Microservices.Auction.Services.Services
{
    internal class CustomerService:ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerResponse> Get(int userId, CancellationToken cancellationToken = default)
        {
            var customerResponse = new CustomerResponse()
            {
                SearchStatus = SearchStatus.BadRequest
            };

            if (userId <= 0)
            {
                return customerResponse;
            }

            var customer = await _unitOfWork._customerRepository.FindByIdAsync(userId, cancellationToken);
            if (customer == null)
            {
                customerResponse.SearchStatus = SearchStatus.NotFound;
                return customerResponse;
            }

            customerResponse.SearchStatus= SearchStatus.Success;
            customerResponse.Customer = _mapper.Map<Domain.DTOs.Customer>(customer);
            return customerResponse;
        }

        public async Task<CustomerResponse> Get(CancellationToken cancellationToken = default)
        {
            var customerResponse = new CustomerResponse()
            {
                SearchStatus = SearchStatus.BadRequest
            };

            var customers = _unitOfWork._customerRepository.GetAll();
            if (customers == null || customers?.Count()==0)
            {
                customerResponse.SearchStatus = SearchStatus.NotFound;
                return customerResponse;
            }

            customerResponse.SearchStatus = SearchStatus.Success;
            customerResponse.Customers = _mapper.Map<IEnumerable<Domain.DTOs.Customer>>(customers);
            return customerResponse;
        }

        public async Task<CustomerResponse> Add(Customer customer, CancellationToken cancellationToken = default)
        {
            var customerResponse = new CustomerResponse()
            {
                CreationStatus = CreationStatus.Unknown
            };

            try
            {
                var existingCustomer =
                    await _unitOfWork._customerRepository.FindByIdAsync(customer.UserID, cancellationToken);
                if (existingCustomer != null)
                {
                    customerResponse.CreationStatus = CreationStatus.Exist;
                    return customerResponse;
                }


                var domainObject = _mapper.Map<Domain.Entities.Customer>(customer);
                await _unitOfWork._customerRepository.Add(domainObject);
                await _unitOfWork.Commit(cancellationToken); 

                customerResponse.CreationStatus = CreationStatus.Success;
                customerResponse.Customer = _mapper.Map<Domain.DTOs.Customer>(domainObject);
                return customerResponse;
            }
            catch (Exception ex)
            {
                customerResponse.CreationStatus = CreationStatus.Error;
                return customerResponse;
            }
        }
    }
}

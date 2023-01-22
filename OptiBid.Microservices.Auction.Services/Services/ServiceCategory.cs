using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Auction.Data.Repositories;
using OptiBid.Microservices.Auction.Domain.DTOs;

namespace OptiBid.Microservices.Auction.Services.Services
{
    public class ServiceCategory:IServiceCategory
    {
        private readonly IRepository<Domain.Entities.ServiceCategory> _repository;
        private readonly IMapper _mapper;

        public ServiceCategory(IRepository<Domain.Entities.ServiceCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Category>> Get(CancellationToken cancellationToken = default)
        {
            return _mapper.Map<IEnumerable<Category>>(_repository.GetAll());
        }
    }
}

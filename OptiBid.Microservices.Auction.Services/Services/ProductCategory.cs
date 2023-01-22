using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OptiBid.Microservices.Auction.Data.Repositories;
using OptiBid.Microservices.Auction.Domain.DTOs;

namespace OptiBid.Microservices.Auction.Services.Services
{
    public class ProductCategory:IProductCategory
    {
        private readonly IRepository<Domain.Entities.ProductCategory> _repository;
        private readonly IMapper _mapper;

        public ProductCategory(IRepository<Domain.Entities.ProductCategory> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Category>> Get(CancellationToken cancellationToken = default)
        {
            return  _mapper.Map<IEnumerable<Category>>(_repository.GetAll());
        }
    }
}

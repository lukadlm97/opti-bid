using AutoMapper;
using Grpc.Core;
using OptiBid.Microservices.Auction.Domain.Entities;
using OptiBid.Microservices.Auction.Grpc.CategoriesServiceDefinition;
using OptiBid.Microservices.Auction.Services.Services;

namespace OptiBid.Microservices.Auction.Grpc.Services
{
    public class CategoryService:Category.CategoryBase
    {
        private readonly IServiceCategory _serviceCategory;
        private readonly IProductCategory _productCategory;
        private readonly IMapper _mapper;

        public CategoryService(IProductCategory productCategory,IServiceCategory serviceCategory,IMapper mapper)
        {
            _serviceCategory = serviceCategory;
            _productCategory = productCategory;
            _mapper = mapper;
        }
        public override async Task<CollectionReplay> GetProducts(EmptyRequest request, ServerCallContext context)
        {
            return new CollectionReplay()
            {
                Categories =
                {
                    _mapper.Map<IEnumerable<Domain.DTOs.Category>, IEnumerable<CategoriesServiceDefinition.DataReply>>(
                        await _productCategory.Get(context.CancellationToken))
                }

            };
        }

        public override async Task<CollectionReplay> GetServices(EmptyRequest request, ServerCallContext context)
        {
            return new CollectionReplay()
            {
                Categories =
                {
                    _mapper.Map<IEnumerable<Domain.DTOs.Category>, IEnumerable<CategoriesServiceDefinition.DataReply>>(
                        await _serviceCategory.Get(context.CancellationToken))
                }

            };
        }
    }
}

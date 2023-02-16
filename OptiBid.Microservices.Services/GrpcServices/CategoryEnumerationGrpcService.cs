using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Auction.Grpc.CategoriesServiceDefinition;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Services.Factory;

namespace OptiBid.Microservices.Services.GrpcServices
{
    public class CategoryEnumerationGrpcService:ICategoryEnumerationGrpcService
    {
        private readonly IAuctionGrpcFactory _auctionGrpcFactory;
        private readonly ILogger<CategoryEnumerationGrpcService> _logger;

        public CategoryEnumerationGrpcService(IAuctionGrpcFactory auctionGrpcFactory,ILogger<CategoryEnumerationGrpcService> logger)
        {
            _auctionGrpcFactory = auctionGrpcFactory;
            _logger = logger;
        }
        public async Task<IEnumerable<EnumItem>> GetProducts(CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetCategoryClient();
                var replay = await grpcClient.GetProductsAsync(new EmptyRequest());
                return replay.Categories.Select(x => new EnumItem() { ID = x.Id, Name = x.Name });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<EnumItem>> GetCategories(CancellationToken cancellationToken = default)
        {
            try
            {
                var grpcClient = _auctionGrpcFactory.GetCategoryClient();
                var replay = await grpcClient.GetServicesAsync(new EmptyRequest());
                return replay.Categories.Select(x => new EnumItem() { ID = x.Id, Name = x.Name });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}

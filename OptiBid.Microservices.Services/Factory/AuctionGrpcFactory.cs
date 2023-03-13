using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Auction.Grpc.AuctionAssetsServiceDefinition;
using OptiBid.Microservices.Auction.Grpc.CategoriesServiceDefinition;
using OptiBid.Microservices.Auction.Grpc.CustomersServiceDefinition;
using OptiBid.Microservices.Contracts.Configuration;

namespace OptiBid.Microservices.Services.Factory
{
    public class AuctionGrpcFactory:IAuctionGrpcFactory
    {
        private ExternalGrpcSettings _grpcSettings;
        private Category.CategoryClient _categoryClient;
        private AuctionAssets.AuctionAssetsClient _assetsClient;
        private Customer.CustomerClient _customerClient;
        public  AuctionGrpcFactory(IOptions<ExternalGrpcSettings> grpcSettings)
        {
            _grpcSettings = grpcSettings.Value;
            CreateCategoryClient();
        }
        private void CreateCategoryClient()
        {
            _categoryClient = new Category.CategoryClient(GrpcChannel.ForAddress(_grpcSettings.AuctionServiceUrl));
        }
        public Category.CategoryClient GetCategoryClient()
        {
            if (_categoryClient == null)
            {
                CreateCategoryClient();
            }

            return _categoryClient;
        }

        private void CreateCustomerClient()
        {
            _customerClient = new Customer.CustomerClient(GrpcChannel.ForAddress(_grpcSettings.AuctionServiceUrl));
        }

        public Customer.CustomerClient GetCustomerClient()
        {
            if (_customerClient == null)
            {
                CreateAuctionAssetsClient();
            }

            return _customerClient;
        }
        private void CreateAuctionAssetsClient()
        {
            _assetsClient = new AuctionAssets.AuctionAssetsClient(GrpcChannel.ForAddress(_grpcSettings.AuctionServiceUrl));
        }
        public AuctionAssets.AuctionAssetsClient GetAuctionAssetsClient()
        {
            if (_assetsClient == null)
            {
                CreateAuctionAssetsClient();
            }

            return _assetsClient;
        }
    }
}

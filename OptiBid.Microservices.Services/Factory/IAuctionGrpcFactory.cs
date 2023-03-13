using OptiBid.Microservices.Auction.Grpc.AuctionAssetsServiceDefinition;
using OptiBid.Microservices.Auction.Grpc.CategoriesServiceDefinition;
using OptiBid.Microservices.Auction.Grpc.CustomersServiceDefinition;

namespace OptiBid.Microservices.Services.Factory
{
    public interface IAuctionGrpcFactory
    {
        Category.CategoryClient GetCategoryClient();
        AuctionAssets.AuctionAssetsClient GetAuctionAssetsClient();
        Customer.CustomerClient GetCustomerClient();
    }
}

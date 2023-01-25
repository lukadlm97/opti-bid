using Microsoft.Extensions.DependencyInjection;
using OptiBid.Microservices.Auction.Data.Repositories;
using OptiBid.Microservices.Auction.Services.Services;
using OptiBid.Microservices.Auction.Services.Utilities;

namespace OptiBid.Microservices.Auction.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddScoped(typeof(IAuctionAssetsRepository), typeof(AuctionAssetsRepository));

            services.AddScoped(typeof(IFireForgetHandler), typeof(FireForgetHandler));

            services.AddScoped(typeof(IProductCategory), typeof(ProductCategory));
            services.AddScoped(typeof(IServiceCategory), typeof(ServiceCategory));
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
            services.AddScoped(typeof(IAuctionAssetService), typeof(AuctionAssetService));


            services.AddScoped<UnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Infrastructure.GrpcServices;
using OptiBid.Microservices.Infrastructure.Services;
using OptiBid.Microservices.Services.Factory;
using OptiBid.Microservices.Services.GrpcServices;
using OptiBid.Microservices.Services.Services;

namespace OptiBid.Microservices.Services.Utilities
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAccountApplicationModules(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IAccountGrpcFactory), typeof(AccountGrpcFactory));
            serviceCollection.AddScoped(typeof(IAccountEnumerationGrpcService), typeof(AccountEnumerationGrpcService));
            serviceCollection.AddScoped(typeof(IAccountGrpcService), typeof(UserGrpcService));
            serviceCollection.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));
            serviceCollection.AddScoped<IJwtManager, JwtManager>();
            serviceCollection.AddScoped<IAccountService, AccountService>();
        }
        public static void AddAuctionApplicationModules(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IAuctionGrpcFactory), typeof(AuctionGrpcFactory));
            serviceCollection.AddScoped<IAuctionAssetService, AuctionAssetService>();
            serviceCollection.AddScoped<IAuctionAssetsGrpcService, AuctionAssetsGrpcService>();

            serviceCollection.AddScoped(typeof(IFireForget<>), typeof(FireForget<>));
        }
        public static void AddSharedApplicationModules(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(ICategoryEnumerationGrpcService), typeof(CategoryEnumerationGrpcService));
            serviceCollection.AddScoped(typeof(ICategoryDashboardService), typeof(CategoryService));

            serviceCollection.AddScoped(typeof(IFireForget<>), typeof(FireForget<>)); 
            serviceCollection.AddScoped(typeof(ICategoryDashboardService), typeof(CategoryService));

        }

    }
}

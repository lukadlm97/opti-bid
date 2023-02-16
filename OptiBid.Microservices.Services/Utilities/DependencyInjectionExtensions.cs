using Microsoft.Extensions.DependencyInjection;
using OptiBid.Microservices.Contracts.GrpcServices;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.Factory;
using OptiBid.Microservices.Services.GrpcServices;
using OptiBid.Microservices.Services.Services;

namespace OptiBid.Microservices.Services.Utilities
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAccountApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IAccountGrpcFactory), typeof(AccountGrpcFactory));
            serviceCollection.AddScoped(typeof(IAuctionGrpcFactory), typeof(AuctionGrpcFactory));
            serviceCollection.AddScoped(typeof(IAccountEnumerationGrpcService), typeof(AccountEnumerationGrpcService));
            serviceCollection.AddScoped(typeof(ICategoryEnumerationGrpcService), typeof(CategoryEnumerationGrpcService));
            serviceCollection.AddScoped(typeof(ICategoryDashboardService), typeof(CategoryService));
            serviceCollection.AddScoped(typeof(IAccountGrpcService), typeof(UserGrpcService));
            serviceCollection.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));

            serviceCollection.AddScoped(typeof(IFireForget<>), typeof(FireForget<>));
            
        }
    }
}

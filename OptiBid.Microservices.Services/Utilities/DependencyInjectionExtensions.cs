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
            serviceCollection.AddScoped(typeof(IAccountEnumerationGrpcService), typeof(AccountEnumerationGrpcService));
            serviceCollection.AddScoped(typeof(IAccountDashboardService), typeof(AccountDashboardService));
            serviceCollection.AddScoped(typeof(IFireForget<>), typeof(FireForget<>));
            
        }
    }
}

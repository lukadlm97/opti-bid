using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OptiBid.Microservices.Accounts.Data.Repository;
using OptiBid.Microservices.Accounts.Services.Utility;

namespace OptiBid.Microservices.Accounts.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
            services.AddScoped(typeof(IAccountAssets), typeof(AccountAssetsRepository));
            services.AddScoped(typeof(IFireForgetHandler), typeof(FireForgetHandler));

            services.AddScoped<UnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}

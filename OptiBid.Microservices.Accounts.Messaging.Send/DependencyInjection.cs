using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Messaging.Send.Sender;

namespace OptiBid.Microservices.Accounts.Messaging.Send
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessageProducing(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAccountSender), typeof(AccountSender));

            return services;
        }

    }
}

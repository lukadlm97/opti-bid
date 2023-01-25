using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Auction.Messaging.Sender.Sender;

namespace OptiBid.Microservices.Auction.Messaging.Sender
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessageProducing(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAuctionAssetsSender), typeof(AuctionAssetsSender));

            return services;
        }

    }
}

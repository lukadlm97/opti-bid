using Microsoft.Extensions.DependencyInjection;
using OptiBid.Microservices.Auction.Messaging.Sender.Factory;
using OptiBid.Microservices.Auction.Messaging.Sender.Sender;

namespace OptiBid.Microservices.Auction.Messaging.Sender
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessageProducing(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAuctionAssetsSender), typeof(AuctionAssetsSender));
            services.AddScoped(typeof(IBidSender), typeof(BidSender));
           
           
            services.AddScoped(typeof(IMqConnectionFactory), typeof(RabbitMqConnectionFactory));

            return services;
        }

    }
}

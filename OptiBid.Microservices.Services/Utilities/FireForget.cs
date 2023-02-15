using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Services.Utilities
{
    public class FireForget<T>:IFireForget<T> where T : class
    {
        private readonly IServiceScopeFactory _serviceProvider;
        private readonly ILogger<FireForget<T>> _logger;

        public FireForget(IServiceScopeFactory serviceProvider,ILogger<FireForget<T>> logger)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }
        public void Execute(Func<IHybridCache<T>, Task> asyncWork)
        {
            Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<IHybridCache<T>>();
                    await asyncWork(sender);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            });
        }
    }
}

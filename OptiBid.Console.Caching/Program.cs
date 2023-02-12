
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OptiBid.Console.Caching.Items;
using OptiBid.Microservices.Shared.Caching.InMemory;
using OptiBid.Microservices.Shared.Caching.Utilities;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostingContext, services) =>
    {
        services.AddHybridCaching(hostingContext);
    });


var builder = host.Build();


var localCache = builder.Services.GetRequiredService<ILocalMemoryCache<MockUpItem>>();
var logger = builder.Services.GetRequiredService<ILogger<ILocalMemoryCache<MockUpItem>>>();
var item = new MockUpItem()
{
    DateTime = DateTime.Today.AddDays(-120),
    Id = 1,
    Name = "Luka",
    Price = 200.7525m
};
await localCache.Set("one", item);
var fetchedItem = await localCache.Get("one");
if (fetchedItem == null)
{
    logger.LogInformation("Item not found at cache");
}
else
{
    if (item.Name == fetchedItem.Name)
    {

        logger.LogInformation("Item is found at cache");
    }
}

await Task.Delay(TimeSpan.FromSeconds(15));
fetchedItem = await localCache.Get("one");
if (fetchedItem == null)
{
    logger.LogInformation("Item not found at cache");
}
else
{
    if (item.Name == fetchedItem.Name)
    {

        logger.LogInformation("Item is found at cache");
    }
}

await localCache.Set("one", item);
await localCache.Invalid("one");
fetchedItem = await localCache.Get("one");
if (fetchedItem == null)
{
    logger.LogInformation("Item not found at cache");
}
else
{
    if (item.Name == fetchedItem.Name)
    {

        logger.LogInformation("Item is found at cache");
    }
}

await builder.RunAsync();
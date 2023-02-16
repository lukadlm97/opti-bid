
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OptiBid.Console.Caching.Items;
using OptiBid.Microservices.Shared.Caching.Hybrid;
using OptiBid.Microservices.Shared.Caching.Utilities;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostingContext, services) =>
    {
        services.AddHybridCaching();
    });


var builder = host.Build();


var hybridCache = builder.Services.GetRequiredService<IHybridCache<MockUpItem>>();
var logger = builder.Services.GetRequiredService<ILogger<IHybridCache<MockUpItem>>>();
var item = new MockUpItem()
{
    DateTime = DateTime.Today.AddDays(-120),
    Id = 1,
    Name = "Luka",
    Price = 200.7525m
};
var key = nameof(MockUpItem) + "" + item.Id;
await hybridCache.Set(key, item);
var fetchedItem = await hybridCache.Get(key);
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

await hybridCache.Invalidate(key);
fetchedItem = await hybridCache.Get(key);
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

await hybridCache.Set(key, item);
await Task.Delay(TimeSpan.FromSeconds(15));
fetchedItem = await hybridCache.Get(key);

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
/*
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
await localCache.Invalidate("one");
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

var distributedCache= builder.Services.GetRequiredService<IDistributedCache<MockUpItem>>();
var loggerDistributed = builder.Services.GetRequiredService<ILogger<IDistributedCache<MockUpItem>>>();

await distributedCache.Set("one", item);
var fetchedItem = await distributedCache.Get("one");
if (fetchedItem == null)
{
    loggerDistributed.LogInformation("Item not found at cache");
}
else
{
    if (item.Name == fetchedItem.Name)
    {

        loggerDistributed.LogInformation("Item is found at cache");
    }
}

await distributedCache.Invalidate("one");
fetchedItem = await distributedCache.Get("one");
if (fetchedItem == null)
{
    loggerDistributed.LogInformation("Item not found at cache");
}
else
{
    if (item.Name == fetchedItem.Name)
    {

        loggerDistributed.LogInformation("Item is found at cache");
    }
}
*/




await builder.RunAsync();
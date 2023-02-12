using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text.Json;
using OptiBid.Microservices.Shared.Messaging.DTOs;

 JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { IgnoreNullValues = true };

var url = "http://localhost:5099/notification";
var connection = new HubConnectionBuilder()
    .WithUrl(url)
    .WithAutomaticReconnect()
    .AddJsonProtocol(op => op.PayloadSerializerOptions.IgnoreNullValues = true)
    // .AddMessagePackProtocol()
    .Build();


await connection.StartAsync();

 /*
Task.Run(async () =>
{
    await foreach (var message in (await  connection.StreamAsChannelAsync<Message>("SendAccountUpdate")).ReadAllAsync())
    {
        Console.WriteLine($"{JsonSerializer.Serialize(message, serializerOptions)}");
    }
});
 Task.Run(async () =>
 {
     await foreach (var message in (await connection.StreamAsChannelAsync<Message>("SendAuctionUpdate")).ReadAllAsync())
     {
         Console.WriteLine($"{JsonSerializer.Serialize(message, serializerOptions)}");
     }

 });
 */

 connection.On<string>("ReceiveAccountUpdate", (message) =>
 {
     Console.WriteLine("Account service send: "+message);
 });
 connection.On<string>("ReceiveAuctionUpdate", (message) =>
 {
     Console.WriteLine("Auction service send: "+message);
 });

var response =await connection.InvokeAsync<string>("Subscribe", "account");
 var responseAuc = "default";
 if (new Random().Next() % 2 == 0)
 {
     responseAuc = await connection.InvokeAsync<string>("Subscribe", "auction");
}

Console.WriteLine(response);
Console.WriteLine(responseAuc);


Console.ReadLine();
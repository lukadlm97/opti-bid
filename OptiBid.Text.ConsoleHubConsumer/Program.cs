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
 connection.On<string,int>("ReceiveAuctionAssetUpdate", (message,id) =>
 {
     if (id == -1)
     {
         Console.WriteLine("Auction service send error message: " + message);
     }
     else
     {

         Console.WriteLine("Auction service send: " + message + " \nDetails are available on page for asset:" + id);
     }
 });
 connection.On<string, int,decimal>("ReceiveAuctionBidUpdate", (message, id,price) =>
 {
     if (id == -1)
     {
         Console.WriteLine("Auction service send error message: " + message);
     }
     else
     {

         Console.WriteLine("Auction service send bid message: " + message + " \nDetails for product with price "+price+" are available on page:" + id);
     }
 });


var response =await connection.InvokeAsync<string>("Subscribe", "account");
 var     responseAuc = await connection.InvokeAsync<string>("Subscribe", "auction");
var   responseBid = await connection.InvokeAsync<string>("Subscribe", "bid");


Console.WriteLine(response);
Console.WriteLine(responseAuc);
Console.WriteLine(responseBid);


Console.ReadLine();
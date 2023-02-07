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
    await foreach (var message in (await  connection.StreamAsChannelAsync<Message>("")).ReadAllAsync())
    {
        Console.WriteLine($"{JsonSerializer.Serialize(message, serializerOptions)}");
    }
});*/
 var responseStr = string.Empty;
 var response =await connection.InvokeAsync<string>("Subscribe", "account");

 Console.WriteLine(response);


Console.ReadLine();
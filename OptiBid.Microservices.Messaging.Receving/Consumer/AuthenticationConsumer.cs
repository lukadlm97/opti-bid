﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.Factories;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OptiBid.Microservices.Messaging.Receving.Consumer
{
    public class AuthenticationConsumer:BackgroundService
    {
        private readonly IMqConnectionFactory _mqConnectionFactory;
        private readonly RabbitMqQueueSettings _queueNames;
        private readonly IAccountMessageQueue _messageQueue;
        private readonly ILogger<AuthenticationConsumer> _logger;

        public AuthenticationConsumer(IMqConnectionFactory mqConnectionFactory,
            IOptions<RabbitMqQueueSettings> options,
            IAccountMessageQueue messageQueue,
            ILogger<AuthenticationConsumer> logger)
        {
            _mqConnectionFactory = mqConnectionFactory;
            _queueNames = options.Value;
            _messageQueue = messageQueue;
            _logger = logger;

        }
        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {

            stoppingToken.ThrowIfCancellationRequested();
            var connection = _mqConnectionFactory.GetConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                // received message
                var body = ea.Body.ToArray();
                var content = System.Text.Encoding.UTF8.GetString(body);

                // handle the received message  
                HandleMessage(content);
                channel.BasicAck(ea.DeliveryTag, false);
            };
            channel.BasicConsume(_queueNames.AccountsQueueName, false, consumer);
            return Task.CompletedTask;
        }
        private void HandleMessage(string content)
        {
            _logger.LogInformation($"consumer received {content}");
            _messageQueue.Write(JsonSerializer.Deserialize<Message>(content));
        }

        async Task Consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await _messageQueue.WriteAsync(JsonSerializer.Deserialize<Message>(message), default);
        }
    }
}

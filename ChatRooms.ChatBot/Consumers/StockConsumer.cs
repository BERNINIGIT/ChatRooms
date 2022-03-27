using ChatRooms.ChatBot.Commands;
using ChatRooms.Common;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatRooms.ChatBot.Consumers
{
    public class StockConsumer : ConsumerBase, IHostedService
    {
        protected override string QueueName => "StockQueue";

        public StockConsumer(
            IMediator mediator,
            ConnectionFactory connectionFactory,
            ILogger<StockConsumer> logConsumerLogger,
            ILogger<ConsumerBase> consumerLogger,
            ILogger<RabbitMqClientBase> logger) :
            base(mediator, connectionFactory, consumerLogger, logger)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += OnEventReceived<StockCommand>;
                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                logConsumerLogger.LogCritical(ex, "Error while consuming message");
            }
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}

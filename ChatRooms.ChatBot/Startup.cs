using System;
using System.Reflection;
using ChatRooms.ChatBot.Commands;
using ChatRooms.ChatBot.Commands.Handlers;
using ChatRooms.ChatBot.Consumers;
using ChatRooms.Core.Services.Implementations;
using FirstReact.Core.Services.Contracts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Rmq.Application.Consumer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddHttpClient()
                .AddTransient<HttpHandler>()
                .AddTransient<StockService>()
                .AddTransient<IFileProcessor, FileProcessor>()
                .AddTransient<IRequestHandler<StockCommand, Unit>, StockCommandHandler>()
                .AddHostedService<StockConsumer>()
                
                .AddSingleton(serviceProvider =>
                {
                    var uri = new Uri("amqps://enjkxden:bSJDhP2EUSzAWYEnz5DArNctqZyl4kPa@woodpecker.rmq.cloudamqp.com/enjkxden");
                    return new ConnectionFactory
                    {
                        Uri = uri,
                        DispatchConsumersAsync = true
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}

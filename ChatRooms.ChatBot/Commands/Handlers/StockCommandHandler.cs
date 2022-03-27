using ChatRooms.Core.Models;
using ChatRooms.Core.Services.Implementations;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChatRooms.ChatBot.Commands.Handlers
{
    public class StockCommandHandler : IRequestHandler<StockCommand>
    {
        private readonly ILogger<StockCommandHandler> _logger;
        private readonly StockService _stockService;

        public StockCommandHandler(ILogger<StockCommandHandler> logger
            , StockService stockService
            )
        { 
            _logger = logger; 
            _stockService = stockService; 
        }

        public async Task<Unit> Handle(StockCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("---- Received message: {Message} ----", request.Message);
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44388/hubs/chat")
                .Build();

            await connection.StartAsync();

            _logger.LogInformation("Starting connection. Press Ctrl-C to close.");

            connection.Closed += e =>
            {
                _logger.LogInformation("Connection closed with error: {0}", e);

                return Task.CompletedTask;
            };

            connection.On("ReceiveMessage", async () =>
            {


            });
            string[] splited = request.Message.Split("/stock=");
            string stockId = splited[1];
            var stockInfo = await _stockService.GetStockInfoAsync(stockId);
            string message = $"{stockId.ToUpper()} quote is ${stockInfo.Close} per share";
            await connection.SendAsync("ReceiveMessage", new ChatMessage { User = "Stock Bot", Message = message });


            return await Task.FromResult(Unit.Value);
        }
    }
}

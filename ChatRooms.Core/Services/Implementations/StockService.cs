using ChatRooms.Core.Models;
using FirstReact.Core.Services.Contracts;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ChatRooms.Core.Services.Implementations
{
    public class StockService
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly HttpHandler _httpHandler;
        private readonly ILogger<StockService> _logger;

        public StockService(IFileProcessor fileProcessor, HttpHandler httpHandler, ILogger<StockService> logger)
        {
            _fileProcessor = fileProcessor;
            _httpHandler = httpHandler;
            _logger = logger;
        }

        public async Task<StockInfo> GetStockInfoAsync(string id)
        {
            StockInfo result = new StockInfo();
            var response = await _httpHandler.GetByteResponse($"https://stooq.com/q/l/?s={id}&f=sd2t2ohlcv&h&e=csv");
            var stockInfo = _fileProcessor.ProcessCsvFile(new MemoryStream(response));
            foreach(var item in stockInfo)
            {
                PropertyInfo prop = result.GetType().GetProperty(item.Key, BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                {
                    prop.SetValue(result, item.Value[0], null);
                }
            }
            return result;
        }
    }
}

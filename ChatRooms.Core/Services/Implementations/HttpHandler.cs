using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatRooms.Core.Services.Implementations
{
    public class HttpHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<byte[]> GetByteResponse(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = httpClient.GetAsync(url);
            return await response.Result.Content.ReadAsByteArrayAsync();
        }
    }
}

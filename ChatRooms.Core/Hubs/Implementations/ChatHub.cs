using ChatRooms.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatRooms.Core.Hubs.Implementations
{
    public class ChatHub : Hub
    {
        public async Task ReceiveMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
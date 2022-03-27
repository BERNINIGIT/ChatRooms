using ChatRooms.Core.Models;
using System.Threading.Tasks;

namespace Chatty.Api.Hubs.Contracts
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
    }
}
using ChatRooms.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRooms.Core.Definitions
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
    }
}

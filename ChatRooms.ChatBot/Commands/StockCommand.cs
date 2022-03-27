using MediatR;

namespace ChatRooms.ChatBot.Commands
{
    public class StockCommand : IRequest<Unit>
    {
        public string User { get; set; }

        public string Message { get; set; }
    }
}

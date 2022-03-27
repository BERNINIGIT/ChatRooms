using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ChatRooms.Core.Hubs.Implementations;
using ChatRooms.Core.Models;
using ChatRooms.Core.Services;
using ChatRooms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatty.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly AmqpService _amqpService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(IHubContext<ChatHub> chatHub, AmqpService amqpService, UserManager<ApplicationUser> userManager)
        {
            _chatHub = chatHub ?? throw new ArgumentNullException(nameof(chatHub));
            _amqpService = amqpService ?? throw new ArgumentNullException(nameof(amqpService));
            _userManager = userManager;
        }

        [HttpPost("messages")]
        public async Task Post(ChatMessage message)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            message.User = user.UserName;
            if (message.Message.StartsWith("/stock="))
            {
                _amqpService.PublishMessage(message);
            }           
            await _chatHub.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}

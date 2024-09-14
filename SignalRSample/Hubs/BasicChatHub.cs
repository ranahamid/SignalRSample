using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Data;

namespace SignalRSample.Hubs
{
    public class BasicChatHub:Hub
    {
        private readonly ApplicationDbContext _context;
        public BasicChatHub(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SendMessageToAll(string sender, string message)
        {
            await Clients.All.SendAsync("MessageReceived", sender, message);
        }
        [Authorize]
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            if(!string.IsNullOrEmpty(receiver) && !string.IsNullOrEmpty(message))
            {
                var userId = _context.Users.FirstOrDefault(x => x.Email.ToLower() == receiver.ToLower())?.Id;
                if (userId != null)
                {
                    await Clients.User(userId).SendAsync("MessageReceived", sender, message);
                }
            }
           
        }
    }
}

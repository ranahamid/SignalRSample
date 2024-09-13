using Microsoft.AspNetCore.SignalR;
using SignalRSample.Data;

namespace SignalRSample.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ApplicationDbContext _context;
        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SendMessageToAll(string sender, string message)
        {
            await Clients.All.SendAsync("MessageReceived", sender, message);
        }
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            var userId = _context.Users.FirstOrDefault(x => x.Email == receiver).Id;
            if(userId != null)
            {
                await Clients.User(userId).SendAsync("MessageReceived", sender, message);
            } 
        }
    }
}

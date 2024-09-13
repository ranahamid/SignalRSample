using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessageToAll(string sender, string message)
        {
            await Clients.All.SendAsync("MessageReceived", sender, message);
        }
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            await Clients..SendAsync("MessageReceived", user, message);
        }
    }
}

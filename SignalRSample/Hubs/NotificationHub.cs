using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class NotificationHub : Hub
    {
        public static int notificationCount { get; set; } = 0;
        public static List<string> messages { get; set; } = new List<string>();

        public async Task SendMessage(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                messages.Add(msg);
                notificationCount++;
                await LoadMessages();
            }
        }
        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotification", messages, notificationCount);
        }
    }
}

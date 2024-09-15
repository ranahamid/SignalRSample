using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SignalRSample.Data;

namespace SignalRSample.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User != null)
            {
                var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userId))
                {
                    var identityUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                    if (identityUser != null)
                    {
                        var userName = identityUser.UserName;
                        var allOnlineUsers = HubConnections.OnlineUsers();
                        Clients.Users(allOnlineUsers).SendAsync("ReceiveUserConnected", userId, userName);
                        HubConnections.AddUserConnection(userId, Context.ConnectionId);
                    }
                }
            }
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User != null)
            {
                var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userId))
                {
                    var connectionId = Context.ConnectionId;
                    if (HubConnections.HasUserConnection(userId, connectionId))
                    {
                        var userConnections = HubConnections.Users[userId];
                        userConnections.Remove(connectionId);

                        HubConnections.Users.Remove(userId);


                        if (userConnections.Any())
                        {
                            HubConnections.Users.Add(userId, userConnections);
                        }

                       
                    }
                    var identityUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                    if (identityUser != null)
                    {
                        var userName = identityUser.UserName;
                        var allOnlineUsers = HubConnections.OnlineUsers();
                        Clients.Users(allOnlineUsers).SendAsync("ReceiveUserDisconnected", userId, userName);
                        HubConnections.AddUserConnection(userId, connectionId);
                    }
                }
            }
            return base.OnConnectedAsync();
        }
         
        public async Task SendAddRoomMessage(int maxRoom, int roomId, string roomName)
        {
            if (Context.User != null)
            {
                var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var identityUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                if (identityUser != null)
                {
                    var userName = identityUser.UserName;
                    await Clients.All.SendAsync("ReceiveAddRoomMessage", maxRoom, roomId, roomName, userId, userName);
                }
            }
        }


        public async Task SendDeleteRoomMessage(int deleted, int selected, string roomName)
        {
            if (Context.User != null)
            {
                var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var identityUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                if (identityUser != null)
                {
                    var userName = identityUser.UserName;

                    await Clients.All.SendAsync("ReceiveDeleteRoomMessage", deleted, selected, roomName, userId, userName);
                }
            }
        }



        public async Task SendPublicMessage(int roomId, string message, string roomName)
        {
            if (Context.User != null)
            {
                var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var identityUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                if (identityUser != null)
                {
                    var userName = identityUser.UserName;

                    await Clients.All.SendAsync("ReceivePublicMessage", message, roomId, roomName, userId, userName);
                }
            }
        }
        public async Task SendPrivateMessage(string receiverId, string receiverName, string message)
        {
            if (Context.User != null)
            {
                var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var identityUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                if (identityUser != null)
                {
                    var userName = identityUser.UserName;

                    var users = new string[] { userId, receiverId };
                    var chatId = Guid.NewGuid();
                    await Clients.Users(users).SendAsync("ReceivePrivateMessage", receiverId, receiverName, message, userId, userName, chatId);
                }
            }
        }

        public async Task SendOpenPrivateChat(string receiverId)
        {
            if (Context.User != null)
            {
                var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var identityUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                if (identityUser != null)
                {
                    var userName = identityUser.UserName;
                    await Clients.User(receiverId).SendAsync("ReceiveOpenPrivateChat", userId, userName);
                }
            }
        }
         
        public async Task SendDeletePrivateChat(string chatId)
        {
            await Clients.All.SendAsync("ReceiveDeletePrivateChat", chatId);
        }



    }
}

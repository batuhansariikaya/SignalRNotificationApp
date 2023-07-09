using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace SignalRNotificationApp.Hubs
{
    public class NotifyHub : Hub
    {
        static List<string> clients = new List<string>();

        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("sendNotfy", message);
        }
        public async Task SendNotificationOthers(string message)
        {
            await Clients.Others.SendAsync("sendNotfyOthers", message);
        }
        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            await Clients.All.SendAsync("clients", clients);
            await Clients.All.SendAsync("userJoined", $"{Context.ConnectionId}");

        }
        // diğerlerine bildirim giderken caller bildirim alıyor.
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clients.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("clients", clients);
            await Clients.All.SendAsync("userLeaved", $"{Context.ConnectionId}");
        }
        // Group
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task GroupSendMessage(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("receiveMessage", message);
        }
    }
}

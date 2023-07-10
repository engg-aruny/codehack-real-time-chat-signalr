using Microsoft.AspNetCore.SignalR;

namespace codehack_realtime_chat_signalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage chatMessage)
        {
            await Clients.All.SendAsync("ReceiveMessage", chatMessage);
        }
    }

    public class ChatMessage
    {
        public string Sender { get; set; }

        public string Content { get; set; }
    }
}

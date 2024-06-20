using Microsoft.AspNetCore.SignalR;
using Smart.Shared.DTOs.ChatDTO;
using System.Threading.Tasks;

namespace Smart.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessageDTO chatMessageDTO)
        {
            await Clients.All.SendAsync("ReceiveMessage", chatMessageDTO);
        }
    }
}

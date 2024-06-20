using Smart.Core.Entities;
using Smart.Shared.DTOs.ChatDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Interfaces.Services
{
    public interface IChatService
    {
        Task<IEnumerable<ChatMessageDTO>> GetChatMessages(int projectId, string userId);
        Task SendMessage(SendMessageDto messageDto);
    }
}

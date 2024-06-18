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
        Task<IEnumerable<ChatMessage>> GetChatMessages(int projectId);
        Task SendMessage(string userId, int projectId, string message);
    }
}

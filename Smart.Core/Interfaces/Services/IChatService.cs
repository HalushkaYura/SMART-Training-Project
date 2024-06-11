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
        Task<ChatDTO> GetChatByProjectIdAsync(int projectId);
        Task<ChatDTO> CreateChatAsync(int projectId);
        Task<ChatMessageDTO> AddMessageAsync(ChatMessageCreateDTO messageDto);
        Task<IEnumerable<ChatMessageDTO>> GetMessagesByChatIdAsync(int chatId);
    }
}

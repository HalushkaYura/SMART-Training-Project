using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Smart.Core.Entities;
using Smart.Core.Interfaces.Repository;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.ChatDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepository<Chat> _chatRepository;
        private readonly IRepository<ChatMessage> _chatMessageRepository;

        public ChatService(IRepository<Chat> chatRepository, IRepository<ChatMessage> chatMessageRepository)
        {
            _chatRepository = chatRepository;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<ChatDTO> GetChatByProjectIdAsync(int projectId)
        {
            var chat = await _chatRepository.GetEntityAsync(c => c.ProjectId == projectId);
            if (chat == null)
                return null;

            return new ChatDTO
            {
                ChatId = chat.ChatId,
                ProjectId = chat.ProjectId,
                CreatedDate = chat.CreatedDate,
                ChatMessages = chat.ChatMessages.Select(m => new ChatMessageDTO
                {
                    ChatMessageId = m.ChatMessageId,
                    ChatId = m.ChatId,
                    UserId = m.UserId,
                    Content = m.Content,
                    SentDate = m.SentDate
                }).ToList()
            };
        }

        public async Task<ChatDTO> CreateChatAsync(int projectId)
        {
            var chat = new Chat
            {
                ProjectId = projectId,
                CreatedDate = DateTime.UtcNow
            };

            await _chatRepository.AddAsync(chat);
            await _chatMessageRepository.SaveChangesAsync();
            return new ChatDTO
            {
                ChatId = chat.ChatId,
                ProjectId = chat.ProjectId,
                CreatedDate = chat.CreatedDate
            };
        }

        public async Task<ChatMessageDTO> AddMessageAsync(ChatMessageCreateDTO messageDto)
        {
            //_logger.LogInformation("Adding new message to chat {ChatId}", messageDto.ChatId);
            var message = new ChatMessage
            {
                ChatId = messageDto.ChatId,
                UserId = messageDto.UserId,
                Content = messageDto.Content,
                SentDate = DateTime.UtcNow
            };

            await _chatMessageRepository.AddAsync(message);
            await _chatMessageRepository.SaveChangesAsync();

            //_logger.LogInformation("Message added with ID {MessageId}", message.ChatMessageId);

            return new ChatMessageDTO
            {
                ChatMessageId = message.ChatMessageId,
                ChatId = message.ChatId,
                UserId = message.UserId,
                Content = message.Content,
                SentDate = message.SentDate
            };
        }


        public async Task<IEnumerable<ChatMessageDTO>> GetMessagesByChatIdAsync(int chatId)
        {
            var messages = await _chatMessageRepository.GetListAsync(m => m.ChatId == chatId);
            return messages.Select(m => new ChatMessageDTO
            {
                ChatMessageId = m.ChatMessageId,
                ChatId = m.ChatId,
                UserId = m.UserId,
                Content = m.Content,
                SentDate = m.SentDate
            }).ToList();
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessages(int projectId)
        {
            //var projectChat = await _dbContext.ProjectChats
            //    .Include(pc => pc.ChatMessages)
            //    .ThenInclude(cm => cm.User)
            //    .FirstOrDefaultAsync(pc => pc.ProjectId == projectId);

            //return projectChat?.ChatMessages;
            throw new NotImplementedException();

        }

        public async Task SendMessage(string userId, int projectId, string message)
        {
            //var user = await _dbContext.Users.FindAsync(userId);
            //var projectChat = await _dbContext.ProjectChats
            //    .Include(pc => pc.Project)
            //    .ThenInclude(p => p.UserProjects)
            //    .ThenInclude(up => up.User)
            //    .FirstOrDefaultAsync(pc => pc.ProjectId == projectId);

            //var chatMessage = new ChatMessage
            //{
            //    Message = message,
            //    SentAt = DateTime.UtcNow,
            //    UserId = userId,
            //    ProjectChatId = projectChat.ProjectChatId
            //};

            //_dbContext.ChatMessages.Add(chatMessage);
            //await _dbContext.SaveChangesAsync();

            //var connections = projectChat.Project.UserProjects.Select(up => up.User.Id).ToList();
            //await _hubContext.Clients.Users(connections).SendAsync("ReceiveMessage", new
            //{
            //    Message = message,
            //    SentAt = chatMessage.SentAt,
            //    UserName = $"{user.Firstname} {user.Lastname}"
            //});
        }
    }
}
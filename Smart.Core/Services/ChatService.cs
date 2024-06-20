using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Smart.Core.Entities;
using Smart.Core.Helpers.Chats;
using Smart.Core.Interfaces.Repository;
using Smart.Core.Interfaces.Services;
using Smart.Server.Hubs;
using Smart.Shared.DTOs.ChatDTO;

public class ChatService : IChatService
{
    private readonly IHubContext<ChatHub> _hubContext;
    protected readonly UserManager<User> _userManager;
    protected readonly IRepository<Chat> _chatRepository;
    protected readonly IRepository<ChatMessage> _chatMessageRepository;

    public ChatService(
        IHubContext<ChatHub> hubContext,
        UserManager<User> userManager,
        IRepository<Chat> chatRepository,
        IRepository<ChatMessage> chatMessageRepository)
    {
        _hubContext = hubContext;
        _userManager = userManager;
        _chatRepository = chatRepository;
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<IEnumerable<ChatMessageDTO>> GetChatMessages(int projectId, string userId)
    {
        var projectChat = await GetProjectChat(projectId);
        var user = await _userManager.FindByIdAsync(userId);

        return projectChat?.ChatMessages.Select(cm => new ChatMessageDTO
        {
            ChatMessageId = cm.ChatMessageId,
            ChatId = cm.ChatId,
            UserId = cm.UserId,
            Content = cm.Content,
            SentDate = cm.SentDate,
            UserFullName = $"{cm.User.Firstname} {cm.User.Lastname}"
        });
    }

    public async Task SendMessage(SendMessageDto messageDto)
    {
        var user = await _userManager.FindByIdAsync(messageDto.UserId);
        var projectChat = await GetProjectChat(messageDto.ProjectId);

        var chatMessage = new ChatMessage
        {
            Content = messageDto.Message,
            SentDate = DateTime.UtcNow,
            UserId = messageDto.UserId,
            ChatId = projectChat.ChatId
        };

        await _chatMessageRepository.AddAsync(chatMessage);
        await _chatMessageRepository.SaveChangesAsync();

        var connections = projectChat.Project.UserProjects.Select(up => up.User.Id).ToList();
        var chatMessageDTO = new ChatMessageDTO
        {
            ChatMessageId = chatMessage.ChatMessageId,
            ChatId = chatMessage.ChatId,
            UserId = chatMessage.UserId,
            Content = chatMessage.Content,
            SentDate = chatMessage.SentDate,
            UserFullName = $"{user.Firstname} {user.Lastname}"
        };

        await _hubContext.Clients.Users(connections).SendAsync("ReceiveMessage", chatMessageDTO);
    }

    private async Task<Chat> GetProjectChat(int projectId)
    {
        return await _chatRepository.GetFirstBySpecAsync(new ChatWithProjectAndMessagesSpec(projectId));
    }
}

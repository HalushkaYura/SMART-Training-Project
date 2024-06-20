using Microsoft.AspNetCore.Mvc;
using Smart.Core.Entities;
using Smart.Core.Interfaces.Services;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetChatMessages(int projectId)
    {
        var chatMessages = await _chatService.GetChatMessages(projectId, UserId);
        return Ok(chatMessages);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto messageDto)
    {
        messageDto.UserId = UserId; 
        await _chatService.SendMessage(messageDto);
        return Ok();
    }
}

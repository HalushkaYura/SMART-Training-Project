using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.ChatDTO;

namespace Smart.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<ChatDTO>> GetChatByProjectId(int projectId)
        {
            var chat = await _chatService.GetChatByProjectIdAsync(projectId);
            if (chat == null)
                return NotFound();

            return Ok(chat);
        }

        [HttpPost("project/{projectId}")]
        public async Task<ActionResult<ChatDTO>> CreateChat(int projectId)
        {
            var chat = await _chatService.CreateChatAsync(projectId);
            return CreatedAtAction(nameof(GetChatByProjectId), new { projectId = chat.ProjectId }, chat);
        }

        [HttpPost("message")]
        public async Task<ActionResult<ChatMessageDTO>> AddMessage([FromBody] ChatMessageCreateDTO messageDto)
        {
            var message = await _chatService.AddMessageAsync(messageDto);
            return CreatedAtAction(nameof(GetMessagesByChatId), new { chatId = message.ChatId }, message);
        }


        [HttpGet("{chatId}/messages")]
        public async Task<ActionResult<IEnumerable<ChatMessageDTO>>> GetMessagesByChatId(int chatId)
        {
            var messages = await _chatService.GetMessagesByChatIdAsync(chatId);
            return Ok(messages);
        }
    }
}

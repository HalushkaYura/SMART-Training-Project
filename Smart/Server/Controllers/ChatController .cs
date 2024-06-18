using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.ChatDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetChatMessages(int projectId)
        {
            var chatMessages = await _chatService.GetChatMessages(projectId);
            return Ok(chatMessages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto messageDto)
        {
            await _chatService.SendMessage(messageDto.UserId, messageDto.ProjectId, messageDto.Message);
            return Ok();
        }
    }
        }

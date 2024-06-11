using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interfaces.Services;
using System.Security.Claims;

namespace Smart.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController  : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet("{workItemId}")]
        public async Task<IActionResult> GetAttachmentsForWorkItem(int workItemId)
        {
            var attachments = await _attachmentService.GetAttachmentsForWorkItemAsync(workItemId);
            return Ok(attachments);
        }

        [HttpPost("{workItemId}")]
        public async Task<IActionResult> AddAttachment(IFormFile file, int workItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var attachment = await _attachmentService.AddAttachmentAsync(file, workItemId, userId);
            return Ok(attachment);
        }

        [HttpDelete("{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(int attachmentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _attachmentService.DeleteAttachmentAsync(attachmentId, userId);
            return success ? Ok() : Forbid();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.CommentDTO;

namespace Smart.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CommentCreateDTO commentDto)
        {
            var comment = await _commentService.AddCommentAsync(commentDto);
            return CreatedAtAction(nameof(GetCommentsByTaskId), new { taskId = comment.TaskId }, comment);
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsByTaskId(int taskId)
        {
            var comments = await _commentService.GetCommentsByTaskIdAsync(taskId);
            return Ok(comments);
        }
    }
}

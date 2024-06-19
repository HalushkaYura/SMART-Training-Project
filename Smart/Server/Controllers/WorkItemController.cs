using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.TaskDTO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Smart.ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IWorkItemService _workItemService;
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public WorkItemController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkItem([FromBody] WorkItemCreateDTO workItemDto)
        {
            try
            {
                var workItemInfo = await _workItemService.CreateWorkItemAsync(workItemDto, UserId);
                return Ok(workItemInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpGet("{workItemId}")]
        public async Task<IActionResult> GetWorkItemById(int workItemId)
        {
            var workItemInfo = await _workItemService.GetWorkItemByIdAsync(workItemId);
            return Ok(workItemInfo);
        }

        [Authorize]
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetWorkItemsByProjectId(int projectId)
        {
            var workItems = await _workItemService.GetWorkItemsByProjectIdAsync(projectId);
            return Ok(workItems);
        }

        [Authorize]
        [HttpPut("edit/{workItemId}")]
        public async Task<IActionResult> UpdateWorkItem([FromBody] WorkItemInfoDTO workItemDto, int workItemId)
        {
            await _workItemService.UpdateWorkItemAsync(workItemDto, workItemId);
            return Ok();
        }
        [Authorize]
        [HttpPut("update-progress/{workItemId}")]
        public async Task<IActionResult> UpdateWorkItemProgress(int workItemId, [FromBody] WorkItemUpdateProgressDTO updateProgressDto)
        {
            await _workItemService.UpdateWorkItemProgressAsync(workItemId, updateProgressDto.Progress);
            return Ok();
        }

        [Authorize]
        [HttpPut("update-status/{workItemId}")]
        public async Task<IActionResult> UpdateWorkItemStatus(int workItemId, [FromBody] WorkItemUpdateStatusDTO updateStatusDto)
        {
            await _workItemService.UpdateWorkItemStatusAsync(workItemId, updateStatusDto.Status);
            return Ok();
        }

        [Authorize]
        [HttpDelete("delete/{workItemId}")]
        public async Task<IActionResult> DeleteWorkItem(int workItemId)
        {
            await _workItemService.DeleteWorkItemAsync(workItemId);
            return Ok();
        }
    }
}

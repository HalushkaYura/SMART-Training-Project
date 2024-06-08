using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.TaskDTO;
using System.Security.Claims;

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
        [HttpPost]
        public async Task<IActionResult> CreateWorkItem([FromBody] WorkItemCreateDTO workItemDto)
        {
            var workItemInfo = await _workItemService.CreateWorkItemAsync(workItemDto, UserId);
            return Ok(workItemInfo);
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
        [HttpPut]
        public async Task<IActionResult> UpdateWorkItem([FromBody] WorkItemInfoDTO workItemDto)
        {
            await _workItemService.UpdateWorkItemAsync(workItemDto);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{workItemId}")]
        public async Task<IActionResult> DeleteWorkItem(int workItemId)
        {
            await _workItemService.DeleteWorkItemAsync(workItemId);
            return Ok();
        }
    }
}
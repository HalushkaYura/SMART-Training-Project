using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.ProjectDTO;
using System.Security.Claims;

namespace Smart.ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public ProjectController(IProjectService projectService, IBackgroundJobClient backgroundJobClient)
        {
            _projectService = projectService;
            _backgroundJobClient = backgroundJobClient;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProjectAsync([FromBody] ProjectCreateDTO projectCreateDTO)
        {
            var id = await _projectService.CreateNewProjectAsync(projectCreateDTO, UserId);
            return Ok(id);
        }

        [Authorize]
        [HttpPost]
        [Route("add-member/{projectId}")]
        public async Task<IActionResult> AddMemberToProjectAsync(int projectId)
        {
            await _projectService.AddMemberToProjectAsync(UserId, projectId);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("info/{projectId}")]
        public async Task<IActionResult> ProjectInfoAsync(int projectId)
        {
            var projectInfo = await _projectService.InfoFromProjectAsync(projectId, UserId);
            return Ok(projectInfo);
        }

        [Authorize]
        [HttpGet]
        [Route("owner")]
        public async Task<IActionResult> GetProjectsOwnedByUserAsync()
        {
            var projects = await _projectService.GetProjectsOwnedByUserAsync(UserId);
            return Ok(projects);
        }

        [Authorize]
        [HttpGet]
        [Route("member")]
        public async Task<IActionResult> GetProjectsUserIsMemberAsync()
        {
            var projects = await _projectService.GetProjectsUserIsMemberAsync(UserId);
            return Ok(projects);
        }

        [Authorize]
        [HttpPut]
        [Route("editProject/{projectId}")]
        public async Task<IActionResult> EditProject([FromBody] ProjectEditDTO projectEditDTO, int projectId)
        {
            var inviteLink = await _projectService.EditProjectDateAsync(projectEditDTO, projectId, UserId);
            return Ok(new { InviteLink = inviteLink });
        }

        [Authorize]
        [HttpGet]
        [Route("details/{projectId}")]
        public async Task<IActionResult> ProjectTableInfo(int projectId)
        {
            var info = await _projectService.GetProjectDetailsAsync(projectId, UserId);
            return Ok(info);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete/{projectId}")]
        public async Task<IActionResult> DeleteRole(int projectId)
        {
            await _projectService.DeleteProjectAsync(projectId, UserId);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("leave/{projectId}")]
        public async Task<IActionResult> Leave(int projectId)
        {
            await _projectService.LeaveTheProjectAsync(projectId, UserId);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("kicked/{userId}/{projectId}")]
        public async Task<IActionResult> DeleteUserAccount(string userId, int projectId)
        {
            await _projectService.DeleteUserWithProject(userId, projectId, UserId);
            return Ok();
        }
    }
}

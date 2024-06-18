using Smart.Core.DTO.UserDTO;
using Smart.Core.Helpers;
using Smart.Shared.DTOs.ProjectDTO;

namespace Smart.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<ProjectIdDTO> CreateNewProjectAsync(ProjectCreateDTO projectDTO, string userId);
        Task<ProjectInfoDTO> InfoFromProjectAsync(int projectId, string userId);
        Task<string> EditProjectDateAsync(ProjectEditDTO projectEditDTO, int projectId, string userId);
        Task AddMemberToProjectAsync(string userId, int projectId);
        Task<IEnumerable<ProjectForUserDTO>> GetProjectsOwnedByUserAsync(string userId);
        Task<IEnumerable<ProjectForUserDTO>> GetProjectsUserIsMemberAsync(string userId);
        Task<ProjectDetailsDTO> GetProjectDetailsAsync(int projectId, string userId);
        Task DeleteProjectAsync(int projectId, string userId);
        Task LeaveTheProjectAsync(int projectId, string userId);
        Task DeleteUserWithProject(string userId, int projectId, string userActive);



        //Task<(IEnumerable<ProjectForUserDTO> Owners, IEnumerable<ProjectForUserDTO> Members)> GetAllProjectsForUserAsync(string userId);

        //Task CreateNewRoleAsync(int projectId, ProjectCreate2DTO roleCreateDTO);

    }
}

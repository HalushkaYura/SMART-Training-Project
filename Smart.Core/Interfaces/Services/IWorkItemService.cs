using Smart.Shared.DTOs.TaskDTO;
using Smart.Shared.Helpers.Enums;

namespace Smart.Core.Interfaces.Services
{
    public interface IWorkItemService
    {
        Task<WorkItemInfoDTO> CreateWorkItemAsync(WorkItemCreateDTO workItemDto, string userId);
        Task<WorkItemInfoDTO> GetWorkItemByIdAsync(int workItemId);
        Task<IEnumerable<WorkItemInfoDTO>> GetWorkItemsByProjectIdAsync(int projectId);
        Task UpdateWorkItemAsync(WorkItemInfoDTO workItemDto, int workItemId);
        Task DeleteWorkItemAsync(int workItemId);
        Task UpdateWorkItemProgressAsync(int workItemId, int progress);
        Task UpdateWorkItemStatusAsync(int workItemId, WorkItemStatus status);

    }
}

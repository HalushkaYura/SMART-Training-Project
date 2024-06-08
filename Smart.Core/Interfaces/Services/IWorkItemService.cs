using Microsoft.AspNetCore.Identity;
using Smart.Core.Entities;
using Smart.Shared.DTOs.TaskDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Interfaces.Services
{
    public interface IWorkItemService
    {
        Task<WorkItemInfoDTO> CreateWorkItemAsync(WorkItemCreateDTO workItemDto, string userId);
        Task<WorkItemInfoDTO> GetWorkItemByIdAsync(int workItemId);
        Task<IEnumerable<WorkItemInfoDTO>> GetWorkItemsByProjectIdAsync(int projectId);
        Task UpdateWorkItemAsync(WorkItemInfoDTO workItemDto);
        Task DeleteWorkItemAsync(int workItemId);

    }
}

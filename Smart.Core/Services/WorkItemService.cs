using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Smart.Core.Entities;
using Smart.Core.Interfaces.Repository;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.TaskDTO;

namespace Smart.Core.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<WorkItem> _workItemRepository;
        private readonly IRepository<Project> _projectRepository;

        public WorkItemService(IMapper mapper, UserManager<User> userManager, IRepository<WorkItem> workItemRepository, IRepository<Project> projectRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _workItemRepository = workItemRepository;
            _projectRepository = projectRepository;
        }

        public async Task<WorkItemInfoDTO> CreateWorkItemAsync(WorkItemCreateDTO workItemDto, string userId)
        {
            var project = await _projectRepository.GetByKeyAsync(workItemDto.ProjectId);
            if (project == null)
                throw new Exception("Project not found.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            var workItem = _mapper.Map<WorkItem>(workItemDto);
            workItem.CreatedByUser = user;
            //workItem.AssignedUser = user;

            await _workItemRepository.AddAsync(workItem);
            await _workItemRepository.SaveChangesAsync();

            return _mapper.Map<WorkItemInfoDTO>(workItem);
        }

        public async Task<WorkItemInfoDTO> GetWorkItemByIdAsync(int workItemId)
        {
            var workItem = await _workItemRepository.GetByKeyAsync(workItemId);
            if (workItem == null)
                throw new Exception("Work item not found.");

            return _mapper.Map<WorkItemInfoDTO>(workItem);
        }

        public async Task<IEnumerable<WorkItemInfoDTO>> GetWorkItemsByProjectIdAsync(int projectId)
        {
            var workItems = await _workItemRepository.GetListAsync(wi => wi.ProjectId == projectId);
            return _mapper.Map<IEnumerable<WorkItemInfoDTO>>(workItems);
        }

        public async Task UpdateWorkItemAsync(WorkItemInfoDTO workItemDto)
        {
            var workItem = await _workItemRepository.GetByKeyAsync(workItemDto.WorkItemId);
            if (workItem == null)
                throw new Exception("Work item not found.");

            _mapper.Map(workItemDto, workItem);

            await _workItemRepository.UpdateAsync(workItem);
            await _workItemRepository.SaveChangesAsync();
        }

        public async Task DeleteWorkItemAsync(int workItemId)
        {
            var workItem = await _workItemRepository.GetByKeyAsync(workItemId);
            if (workItem == null)
                throw new Exception("Work item not found.");

            await _workItemRepository.DeleteAsync(workItem);
            await _workItemRepository.SaveChangesAsync();
        }
    }
}
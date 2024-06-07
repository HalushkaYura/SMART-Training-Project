using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Smart.Core.Entities;
using Smart.Core.Exeptions;
using Smart.Core.Interfaces.Repository;
using Smart.Core.Interfaces.Services;
using Smart.Core.Resources;
using Smart.Shared.DTOs.ProjectDTO;
using System.Data;


namespace Smart.Core.Services
{
    public class ProjectService : IProjectService
    {
        protected readonly IMapper _mapper;
        protected readonly UserManager<User> _userManager;
        protected IRepository<Project> _projectRepository;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IRepository<UserProject> _userProjectRepository;
        protected readonly IRepository<User> _userRepository;
        //protected readonly INotificationService _notificationService;
        public ProjectService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IRepository<Project> projectRepository,
            IRepository<UserProject> userProjectRepository,
            UserManager<User> userManager,
            IRepository<User> userRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _projectRepository = projectRepository;
            _userProjectRepository = userProjectRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }


        public async Task<ProjectIdDTO> CreateNewProjectAsync(ProjectCreateDTO projectDTO, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.UserNotFound);

            var existingProjects = await _projectRepository.GetListAsync(
                p => p.Name == projectDTO.Name && p.UserProjects.Any(up => up.UserId == userId));

            if (existingProjects.Any())
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Project with the same name already exists for this user.");

            var project = _mapper.Map<Project>(projectDTO);


            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return new ProjectIdDTO { ProjectId = project.ProjectId };
        }
        
        public async Task AddMemberToProjectAsync(string userId, int projectId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.UserNotFound);

            var existingUserProject = await _userProjectRepository.GetListAsync(up => up.UserId == userId && up.ProjectId == projectId);
            if (existingUserProject.Any())
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "User is already a member of this project.");
           
            await CreateUserProject(userId, projectId, false);

        }


        private async Task CreateUserProject(string userId, int projectId, bool isOwner)
        {
            var project = await _projectRepository.GetByKeyAsync(projectId);

            if (project == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.ProjectNotFound);

            var userProject = new UserProject
            {
                IsOwner = isOwner,
                UserId = userId,
                Project = project
            };

            await _userProjectRepository.AddAsync(userProject);
            await _userProjectRepository.SaveChangesAsync();
        }

        public async Task<ProjectInfoDTO> InfoFromProjectAsync(int projectId, string userId)
        {
            var project = await _projectRepository.GetByKeyAsync(projectId);
            if (project == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.ProjectNotFound);

            var userProject = await _userProjectRepository.GetEntityAsync(x => x.UserId == userId && x.ProjectId == projectId);
            bool? isOwner;
            if (userProject == null)
                isOwner = null;
            else
                isOwner = userProject != null && userProject.IsOwner == true;

            var projectInfo = _mapper.Map<ProjectInfoDTO>(project);
            projectInfo.IsOwner = isOwner;

            var userProjectOwer = await _userProjectRepository.GetEntityAsync(x => x.ProjectId == projectId && x.IsOwner == true);
            var userOwner = await _userManager.FindByIdAsync(userProjectOwer.UserId);
            projectInfo.OwnerName = userOwner.Firstname + " " + userOwner.Lastname;
            projectInfo.OwnerURL = userOwner.ImageUrl;
            return projectInfo;
        }

        public async Task EditProjectDateAsync(ProjectEditDTO projectEditDTO, int projectId, string userId)
        {
            var project = await _projectRepository.GetByKeyAsync(projectId);

            if (project == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.ProjectNotFound);

            project.Name = projectEditDTO.Name;

            await _projectRepository.UpdateAsync(project);
            await _projectRepository.SaveChangesAsync();

        }

        public async Task<IEnumerable<ProjectForUserDTO>> GetProjectsOwnedByUserAsync(string userId)
        {
            var userProjects = await _userProjectRepository.GetListAsync(up => up.UserId == userId && up.IsOwner);

            var projectIds = userProjects.Select(up => up.ProjectId);

            var projects = await _projectRepository.GetListAsync(p => projectIds.Contains(p.ProjectId));

            var projectDtos = _mapper.Map<IEnumerable<ProjectForUserDTO>>(projects);

            return projectDtos;
        }

        public async Task<IEnumerable<ProjectForUserDTO>> GetProjectsUserIsMemberAsync(string userId)
        {
            var userProjects = await _userProjectRepository.GetListAsync(up => up.UserId == userId && !up.IsOwner);

            var projectIds = userProjects.Select(up => up.ProjectId);

            var projects = await _projectRepository.GetListAsync(p => projectIds.Contains(p.ProjectId));

            var projectDtos = _mapper.Map<IEnumerable<ProjectForUserDTO>>(projects);

            return projectDtos;
        }
        public async Task<ProjectDetailsDTO> GetProjectDetailsAsync(int projectId, string userId)
        {
            var userProjectTest = await _userProjectRepository.GetEntityAsync(x => x.UserId == userId && x.ProjectId == projectId);
            if (userProjectTest == null || userProjectTest.IsOwner == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.ProjectNotFound);

            var project = await _projectRepository.GetByKeyAsync(projectId);
            if (project == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.ProjectNotFound);

            var projectDTO = _mapper.Map<ProjectDetailsDTO>(project);

            var userProjects = await _userProjectRepository.GetListAsync(x => x.ProjectId == projectId);
            var userProjectUserIds = userProjects.Select(up => up.UserId).ToList();
            var users = await _userRepository.GetListAsync(x => userProjectUserIds.Contains(x.Id));

            var memberDTOs = _mapper.Map<List<ProjectMemberDTO>>(userProjects);
            var usersDTO = _mapper.Map<List<ProjectMemberDTO>>(users);

            for (var i = 0; i < memberDTOs.Count; i++)
            {
                // Знаходимо відповідність між об'єктами за Id
                var userDTO = usersDTO.FirstOrDefault(u => u.UserId == userProjects.ElementAt(i).UserId);

                // Просто присвоюємо значення з мапованих об'єктів
                memberDTOs[i].UserName = userDTO.UserName;
                memberDTOs[i].ImageUrl = userDTO.ImageUrl;
            }

            projectDTO.Members = memberDTOs;
            projectDTO.IsOwner = userProjectTest.IsOwner;

            return projectDTO;
        }
        public async Task DeleteProjectAsync(int projectId, string userId)
        {
            var project = await _projectRepository.GetByKeyAsync(projectId);
            if (project == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.ProjectNotFound);

            var userProject = await _userProjectRepository.GetEntityAsync(x => x.UserId == userId && x.ProjectId == projectId);
            if (userProject == null || userProject.IsOwner != true)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Not enough rights");

            await _projectRepository.DeleteAsync(project);
            await _projectRepository.SaveChangesAsync();
        }
        public async Task LeaveTheProjectAsync(int projectId, string userId)
        {

            var project = await _projectRepository.GetByKeyAsync(projectId);
            if (project == null)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, ErrorMessages.ProjectNotFound);
            var userProject = await _userProjectRepository.GetEntityAsync(x => x.UserId == userId && x.ProjectId == projectId);

            if (userProject == null || userProject.IsOwner == true)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Impossible action");


            await _userProjectRepository.DeleteAsync(userProject);
            await _userProjectRepository.SaveChangesAsync();
        }

        public async Task DeleteUserWithProject(string userId, int projectId, string userActive)
        {
            var userProject = await _userProjectRepository.GetEntityAsync(x => x.UserId == userActive && x.ProjectId == projectId);
            if (userProject.IsOwner != true)
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Not enough rights");
            var userProjectDelete = await _userProjectRepository.GetEntityAsync(x => x.UserId == userId && x.ProjectId == projectId);

            await _userProjectRepository.DeleteAsync(userProjectDelete);
            await _userProjectRepository.SaveChangesAsync();
        }

    }
}

using AutoMapper;
using Smart.Core.Entities;
using Smart.Shared.DTOs.ProjectDTO;
using Smart.Shared.DTOs.TaskDTO;
using Smart.Shared.DTOs.UserDTO;
using System.Data;

namespace Smart.Core.Helpers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<User, UserChangeInfoDTO>().ReverseMap();
            CreateMap<UserRegistrationDTO, User>().ReverseMap();
            CreateMap<User, UserChangeInfoDTO>();

            CreateMap<ProjectCreateDTO, Project>().ReverseMap();
            CreateMap<Project, ProjectInfoDTO>().ReverseMap();
            CreateMap<Project, ProjectEditDTO>();


            CreateMap<ProjectForUserDTO, Project>().ReverseMap();
            CreateMap<Project, ProjectDetailsDTO>().ReverseMap(); 


            CreateMap<User, ProjectMemberDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Firstname} {src.Lastname}"))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserProject, ProjectMemberDTO>();

            CreateMap<WorkItem, WorkItemCreateDTO>().ReverseMap();
            CreateMap<WorkItem, WorkItemEditDTO>().ReverseMap();
            CreateMap<WorkItem, WorkItemInfoDTO>().ReverseMap();

            /*CreateMap<InviteUser, UserInviteInfoDTO>()
                .ForMember(x => x.Id, act => act.MapFrom(srs => srs.Id))
                .ForMember(x => x.Date, act => act.MapFrom(srs => srs.Date))
                .ForMember(x => x.IsConfirm, act => act.MapFrom(srs => srs.IsConfirm))
                .ForMember(x => x.WorkspaceName, act => act.MapFrom(srs => srs.Workspace.Name))
                .ForMember(x => x.FromUserName, act => act.MapFrom(srs => srs.FromUser.Name))
                .ForMember(x => x.ToUserId, act => act.MapFrom(srs => srs.ToUserId));*/


            /* CreateMap<BlobDownloadInfo, DownloadFile>()
                 .ForMember(x => x.ContentType, act => act.MapFrom(srs => srs.Details.ContentType))
                 .ForMember(x => x.Content, act => act.MapFrom(srs => srs.Content));*/

        }
    }
}

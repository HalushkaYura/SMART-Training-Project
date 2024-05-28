using AutoMapper;
using Smart.Server.Entities;
using TaskBoard.Core.DTOs.UserDTO;

namespace TaskBoard.Core.Helpers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //CreateMap<BoardCreateDTO, Board>().ReverseMap();
            //CreateMap<BoardDTO, Board>().ReverseMap();

            //CreateMap<AssignmentCreateDTO, Assignment>().ReverseMap();
            //CreateMap<AssignmentDTO, Assignment>()
            //    .ForMember(x => x.UserId, usr => usr.MapFrom(srs => srs.UserId))
            //    .ReverseMap();

            CreateMap<UserInfoDTO, User>()
                .ForMember(x => x.Id, usr => usr.MapFrom(srs => srs.UserId)).ReverseMap();



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

namespace Smart.Shared.DTOs.ProjectDTO
{
    public class ProjectInfoDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public bool? IsOwner { get; set; }
        public bool IsPublic { get; set; }
        public string? InviteToken {  get; set; }
        public string OwnerName { get; set; }
        public string OwnerURL { get; set; }

    }
}

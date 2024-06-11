namespace Smart.Shared.DTOs.ProjectDTO
{
    public class ProjectDetailsDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public bool? IsOwner { get; set; }
        public List<ProjectMemberDTO> Members { get; set; }
    }
}

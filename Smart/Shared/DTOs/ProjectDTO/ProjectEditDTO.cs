
using System.ComponentModel.DataAnnotations;

namespace Smart.Shared.DTOs.ProjectDTO
{
    public class ProjectEditDTO
    {
        [Required]
        public string Name { get; set; }
        public string InviteToken { get; set; }
        public bool IsPublic { get; set; }

    }
}

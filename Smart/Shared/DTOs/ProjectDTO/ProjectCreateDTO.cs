
using System.ComponentModel.DataAnnotations;

namespace Smart.Shared.DTOs.ProjectDTO
{
    public class ProjectCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

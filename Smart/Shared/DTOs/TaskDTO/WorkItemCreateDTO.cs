using Smart.Shared.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Smart.Shared.DTOs.TaskDTO
{
    public class WorkItemCreateDTO
    {
        public int ProjectId { get; set; } 
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; } 
        [Required]
        public WorkItemStatus Status { get; set; }
        [Required]
        public WorkItemPriority Priority { get; set; }
        [Required]
        public DateTime EndDate { get; set; } 

    }
}

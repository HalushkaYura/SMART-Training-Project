using Smart.Shared.Helpers.Enums;

namespace Smart.Shared.DTOs.TaskDTO
{
    public class WorkItemCreateDTO
    {
        public int ProjectId { get; set; } // Ідентифікатор проекту, до якого належить робочий елемент
        public string Name { get; set; } // Назва робочого елементу
        public string Description { get; set; } // Опис робочого елементу
        public WorkItemStatus Status { get; set; }
        public WorkItemPriority Priority { get; set; }
        public DateTime EndDate { get; set; } // Дата завершення робочого елементу

    }
}

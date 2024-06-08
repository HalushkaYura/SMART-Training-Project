using Smart.Shared.Helpers.Enums;

namespace Smart.Shared.DTOs.TaskDTO
{
    public class WorkItemCreateDTO
    {
        public int ProjectId { get; set; } // Ідентифікатор проекту, до якого належить робочий елемент
        public int? ParentTaskId { get; set; } // Ідентифікатор батьківської задачі (якщо є)
        public string Name { get; set; } // Назва робочого елементу
        public string Description { get; set; } // Опис робочого елементу
        public WorkItemStatus Status { get; set; }
        public WorkItemPriority Priority { get; set; }
        public DateTime StartDate { get; set; } // Дата початку робочого елементу
        public DateTime EndDate { get; set; } // Дата завершення робочого елементу
        public string AssignedUserId { get; set; } // Ідентифікатор користувача, якому призначено робочий елемент
        public string CreatedByUserId { get; set; } // Ідентифікатор користувача, який створив робочий елемент

    }
}

using Smart.Core.Helpers.Enums;
using Smart.Core.Interfaces;

namespace Smart.Core.Entities
{
    public class WorkItem : IBaseEntity
    {
        public int WorkItemId { get; set; } // Ідентифікатор робочого елементу
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

        public Project Project { get; set; } // Проект, до якого належить робочий елемент
        public WorkItem ParentTask { get; set; } // Батьківська задача (якщо є)
        public User AssignedUser { get; set; } // Користувач, якому призначено робочий елемент
        public User CreatedByUser { get; set; } // Користувач, який створив робочий елемент

        public ICollection<WorkItem> SubTasks { get; set; } // Список підзадач робочого елементу

        public ICollection<Comment> Comments { get; set; } // Список коментарів до робочого елементу

        public ICollection<Attachment> Attachments { get; set; } // Список вкладень, пов'язаних з робочим елементом
    }
}

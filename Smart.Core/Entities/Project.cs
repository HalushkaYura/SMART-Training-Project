using Smart.Core.Interfaces;

namespace Smart.Core.Entities
{
    public class Project : IBaseEntity
    {
        public int ProjectId { get; set; } // Ідентифікатор проекту
        public string Name { get; set; } // Назва проекту
        public string Description { get; set; } // Опис проекту
        public bool IsPublic { get; set; } //бачення проекту
        public DateTime StartDate { get; set; } // Дата початку проекту
        public DateTime EndDate { get; set; } // Дата завершення проекту
        public string CreatedByUserId { get; set; } // Ідентифікатор користувача, який створив проект
        public string InviteToken { get; set; }

        public User CreatedByUser { get; set; } // Користувач, який створив проект

        public ICollection<UserProject> UserProjects { get; set; } // Список користувачів, які мають доступ до проекту

        public ICollection<WorkItem> WorkItems { get; set; } // Список робочих елементів проекту

        public ICollection<Chat> Chats { get; set; } // Список чатів, пов'язаних з проектом
    }
}

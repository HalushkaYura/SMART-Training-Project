using Smart.Core.Interfaces;

namespace Smart.Core.Entities
{
    public class UserProject : IBaseEntity
    {
        public string UserId { get; set; } // Ідентифікатор користувача
        public int ProjectId { get; set; } // Ідентифікатор проекту
        public bool IsOwner { get; set; }
        public User User { get; set; } // Пов'язаний користувач
        public Project Project { get; set; } // Пов'язаний проект

    }
}

namespace Smart.Server.Entities
{
    public class UserProject
    {
        public string UserId { get; set; } // Ідентифікатор користувача
        public int ProjectId { get; set; } // Ідентифікатор проекту

        public User User { get; set; } // Пов'язаний користувач
        public Project Project { get; set; } // Пов'язаний проект

    }
}

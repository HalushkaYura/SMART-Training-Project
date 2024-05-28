using Smart.Shared.Interface;

namespace Smart.Server.Entities
{
    public class Comment : IBaseEntity
    {
        public int CommentId { get; set; } // Ідентифікатор коментаря
        public int TaskId { get; set; } // Ідентифікатор робочого елементу, до якого відноситься коментар
        public string UserId { get; set; } // Ідентифікатор користувача, який залишив коментар
        public string Content { get; set; } // Зміст коментаря
        public DateTime CreatedDate { get; set; } // Дата створення коментаря

        public WorkItem WorkItem { get; set; } // Робочий елемент, до якого відноситься коментар
        public User User { get; set; } // Користувач, який залишив коментар

    }
}

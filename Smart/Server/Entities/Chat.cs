using Smart.Shared.Interface;

namespace Smart.Server.Entities
{
    public class Chat : IBaseEntity
    {
        public int ChatId { get; set; } // Ідентифікатор чату
        public int ProjectId { get; set; } // Ідентифікатор проекту, до якого відноситься чат
        public DateTime CreatedDate { get; set; } // Дата створення чату

        public Project Project { get; set; } // Проект, до якого відноситься чат

        public ICollection<ChatMessage> ChatMessages { get; set; } // Повідомлення у чаті

    }
}

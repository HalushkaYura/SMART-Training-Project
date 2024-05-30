using Smart.Core.Interfaces;

namespace Smart.Core.Entities
{
    public class ChatMessage : IBaseEntity
    {
        public int ChatMessageId { get; set; } // Ідентифікатор повідомлення
        public int ChatId { get; set; } // Ідентифікатор чату, до якого відноситься повідомлення
        public string UserId { get; set; } // Ідентифікатор користувача, який написав повідомлення
        public string Content { get; set; } // Зміст повідомлення
        public DateTime SentDate { get; set; } // Дата надіслання повідомлення

        public Chat Chat { get; set; } // Чат, до якого відноситься повідомлення
        public User User { get; set; } // Користувач, який написав повідомлення

    }
}

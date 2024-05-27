namespace Smart.Server.Entities
{
    public class ChatMessage
    {
        public int ChatMessageId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }

        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}

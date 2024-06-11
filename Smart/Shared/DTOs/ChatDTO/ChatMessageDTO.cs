namespace Smart.Shared.DTOs.ChatDTO
{
    public class ChatMessageDTO
    {
        public int ChatMessageId { get; set; }
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
    }
}

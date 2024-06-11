namespace Smart.Shared.DTOs.ChatDTO
{
    public class ChatMessageCreateDTO
    {
        public int ChatId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}

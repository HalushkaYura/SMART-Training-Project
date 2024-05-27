namespace Smart.Server.Entities
{
    public class Chat
    {
        public int ChatId { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Project Project { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}

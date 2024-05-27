namespace Smart.Server.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public WorkItem WorkItem { get; set; }
        public User User { get; set; }
    }
}

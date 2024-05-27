using Microsoft.AspNetCore.Identity;

namespace Smart.Server.Entities
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<WorkItem> CreatedTasks { get; set; }
        public ICollection<WorkItem> AssignedTasks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}

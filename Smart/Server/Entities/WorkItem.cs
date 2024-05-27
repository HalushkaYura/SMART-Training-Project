using System.Net.Mail;
using System.Xml.Linq;

namespace Smart.Server.Entities
{
    public class WorkItem
    {
        public int WorkItemId { get; set; }
        public int ProjectId { get; set; }
        public int? ParentTaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? AssignedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public Project Project { get; set; }
        public WorkItem ParentTask { get; set; }
        public User AssignedUser { get; set; }
        public User CreatedByUser { get; set; }
        public ICollection<WorkItem> SubTasks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}

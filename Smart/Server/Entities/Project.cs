using System;

namespace Smart.Server.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
        public ICollection<Chat> Chats { get; set; }
    }
}

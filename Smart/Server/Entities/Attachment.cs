namespace Smart.Server.Entities
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public int WorkItemId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int UploadedByUserId { get; set; }
        public DateTime UploadedDate { get; set; }

        public WorkItem WorkItem { get; set; }
        public User UploadedByUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.AttachmentDTO
{
    public class AttachmentDTO
    {
        public int AttachmentId { get; set; }
        public int WorkItemId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string UploadedByUserId { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}

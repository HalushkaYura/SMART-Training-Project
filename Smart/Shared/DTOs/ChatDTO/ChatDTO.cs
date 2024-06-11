using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.ChatDTO
{
    public class ChatDTO
    {
        public int ChatId { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<ChatMessageDTO> ChatMessages { get; set; }
    }
}

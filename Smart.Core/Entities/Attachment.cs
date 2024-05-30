using Smart.Core.Interface;

namespace Smart.Core.Entities
{
    public class Attachment : IBaseEntity
    {
        public int AttachmentId { get; set; } // Ідентифікатор вкладення
        public int WorkItemId { get; set; } // Ідентифікатор робочого елементу, до якого відноситься вкладення
        public string FilePath { get; set; } // Шлях до файлу вкладення
        public string FileName { get; set; } // Назва файлу вкладення
        public string UploadedByUserId { get; set; } // Ідентифікатор користувача, який завантажив вкладення
        public DateTime UploadedDate { get; set; } // Дата завантаження вкладення

        public WorkItem AttachedWorkItem { get; set; } // Робочий елемент, до якого відноситься вкладення
        public User UploadedByUser { get; set; } // Користувач, який завантажив вкладення

    }
}

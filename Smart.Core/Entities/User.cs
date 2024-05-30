using Microsoft.AspNetCore.Identity;
using Smart.Core.Entities.RefreshTokenEntity;
using Smart.Core.Interface;

namespace Smart.Core.Entities
{
    public class User : IdentityUser,  IBaseEntity
    {
        public string Firstname { get; set; } // Ім'я користувача
        public string Lastname { get; set; }  // Прізвище користувача
        public DateTime BirthDate { get; set; } // Дата народження користувача
        public string ImageUrl { get; set; } // аватар
        public ICollection<UserProject> UserProjects { get; set; } // Список проектів, до яких має доступ користувач
        public ICollection<WorkItem> CreatedTasks { get; set; } // Задачі, які створив користувач
        public ICollection<WorkItem> AssignedTasks { get; set; } // Задачі, які призначені користувачеві
        public ICollection<Comment> Comments { get; set; } // Коментарі, які залишив користувач
        public ICollection<Attachment> Attachments { get; set; } // Вкладення, які додав користувач
        public ICollection<ChatMessage> ChatMessages { get; set; } // Повідомлення в чатах, які написав користувач
        public ICollection<RefreshToken> RefreshTokens { get; set; } // токени 

    }
}

using Ardalis.Specification;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Smart.Core.Helpers.Chats
{
    public class ChatWithProjectAndMessagesSpec : Specification<Chat>
    {
        public ChatWithProjectAndMessagesSpec(int projectId)
        {
            Query
                .Include(c => c.Project)
                .ThenInclude(p => p.UserProjects)
                .ThenInclude(up => up.User)
                .Include(c => c.ChatMessages)
                .ThenInclude(cm => cm.User)
                .Where(c => c.ProjectId == projectId);
        }
    }
}

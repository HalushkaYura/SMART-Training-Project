using Smart.Shared.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.TaskDTO
{
    public class WorkItemInfoDTO
    {
        public int WorkItemId { get; set; } // Ідентифікатор робочого елементу
        public int ProjectId { get; set; } // Ідентифікатор проекту, до якого належить робочий елемент
        public string Name { get; set; } // Назва робочого елементу
        public string Description { get; set; } // Опис робочого елементу
        public WorkItemStatus Status { get; set; }
        public int Procent {  get; set; }
        public WorkItemPriority Priority { get; set; }
        public DateTime StartDate { get; set; } // Дата початку робочого елементу
        public DateTime EndDate { get; set; } // Дата завершення робочого елементу
        public string AssignedUserId { get; set; } // Ідентифікатор користувача, якому призначено робочий елемент
        public string CreatedByUserId { get; set; } // Ідентифікатор користувача, який створив робочий елемент
    }
}

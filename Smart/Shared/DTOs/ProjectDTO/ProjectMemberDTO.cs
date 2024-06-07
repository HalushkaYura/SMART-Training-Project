using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.ProjectDTO
{
    public class ProjectMemberDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsOwner { get; set; }
    }
}

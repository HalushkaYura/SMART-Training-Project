
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.DTOs.ProjectDTO
{
    public class ProjectInfoDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public bool? IsOwner { get; set; }

        public string OwnerName{ get; set; }
        public string OwnerURL { get; set; }

    }
}

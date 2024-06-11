using Smart.Shared.DTOs.CommentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Interfaces.Services
{
    public interface ICommentService
    {
        Task<CommentDTO> AddCommentAsync(CommentCreateDTO commentDto);
        Task<IEnumerable<CommentDTO>> GetCommentsByTaskIdAsync(int taskId);
    }
}

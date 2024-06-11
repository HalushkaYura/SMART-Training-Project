using Smart.Core.Entities;
using Smart.Core.Interfaces.Repository;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.CommentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDTO> AddCommentAsync(CommentCreateDTO commentDto)
        {
            var comment = new Comment
            {
                TaskId = commentDto.TaskId,
                UserId = commentDto.UserId,
                Content = commentDto.Content,
                CreatedDate = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);
            return new CommentDTO
            {
                CommentId = comment.CommentId,
                TaskId = comment.TaskId,
                UserId = comment.UserId,
                Content = comment.Content,
                CreatedDate = comment.CreatedDate
            };
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsByTaskIdAsync(int taskId)
        {
            var comments = await _commentRepository.GetListAsync(c => c.TaskId == taskId);
            return comments.Select(c => new CommentDTO
            {
                CommentId = c.CommentId,
                TaskId = c.TaskId,
                UserId = c.UserId,
                Content = c.Content,
                CreatedDate = c.CreatedDate
            }).ToList();
        }
    }
}

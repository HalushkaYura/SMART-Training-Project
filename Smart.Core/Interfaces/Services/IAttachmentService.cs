using Microsoft.AspNetCore.Http;
using Smart.Shared.DTOs.AttachmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Interfaces.Services
{
    public interface IAttachmentService
    {
        Task<IEnumerable<AttachmentDTO>> GetAttachmentsForWorkItemAsync(int workItemId);
        Task<AttachmentDTO> AddAttachmentAsync(IFormFile file, int workItemId, string userId);
        Task<bool> DeleteAttachmentAsync(int attachmentId, string userId);
    }
}

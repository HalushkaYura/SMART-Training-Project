using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ServiceStack;
using Smart.Core.Entities;
using Smart.Core.Interfaces.Repository;
using Smart.Core.Interfaces.Services;
using Smart.Shared.DTOs.AttachmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IRepository<Attachment> _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AttachmentService(IRepository<Attachment> unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IEnumerable<AttachmentDTO>> GetAttachmentsForWorkItemAsync(int workItemId)
        {
            var attachments = await _unitOfWork.GetListAsync(a => a.WorkItemId == workItemId);
            return attachments.Select(a => new AttachmentDTO
            {
                AttachmentId = a.AttachmentId,
                WorkItemId = a.WorkItemId,
                FilePath = a.FilePath,
                FileName = a.FileName,
                UploadedByUserId = a.UploadedByUserId,
                UploadedDate = a.UploadedDate
            });
        }

        public async Task<AttachmentDTO> AddAttachmentAsync(IFormFile file, int workItemId, string userId)
        {
            var attachment = new Attachment
            {
                WorkItemId = workItemId,
                UploadedByUserId = userId,
                UploadedDate = DateTime.UtcNow
            };

            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            attachment.FilePath = filePath;
            attachment.FileName = file.FileName;

            await _unitOfWork.AddAsync(attachment);
            await _unitOfWork.SaveChangesAsync();

            return new AttachmentDTO
            {
                AttachmentId = attachment.AttachmentId,
                WorkItemId = attachment.WorkItemId,
                FilePath = attachment.FilePath,
                FileName = attachment.FileName,
                UploadedByUserId = attachment.UploadedByUserId,
                UploadedDate = attachment.UploadedDate
            };
        }

        public async Task<bool> DeleteAttachmentAsync(int attachmentId, string userId)
        {
            var attachment = await _unitOfWork.GetByKeyAsync(attachmentId);

            if (attachment == null || attachment.UploadedByUserId != userId)
            {
                return false;
            }

            _unitOfWork.DeleteAsync(attachment);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}

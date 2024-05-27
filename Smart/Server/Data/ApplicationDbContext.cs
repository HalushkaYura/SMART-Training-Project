using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smart.Server.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace Smart.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування зв'язків
            modelBuilder.Entity<WorkItem>()
                .HasOne(t => t.ParentTask)
                .WithMany(t => t.SubTasks)
                .HasForeignKey(t => t.ParentTaskId);

            modelBuilder.Entity<WorkItem>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedUserId);

            modelBuilder.Entity<WorkItem>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedByUserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.WorkItem)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.WorkItem)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.WorkItemId);

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.UploadedByUser)
                .WithMany(u => u.Attachments)
                .HasForeignKey(a => a.UploadedByUserId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.Chat)
                .WithMany(c => c.ChatMessages)
                .HasForeignKey(cm => cm.ChatId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.ChatMessages)
                .HasForeignKey(cm => cm.UserId);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Project)
                .WithMany(p => p.Chats)
                .HasForeignKey(c => c.ProjectId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.CreatedByUserId);
        }
    }
}

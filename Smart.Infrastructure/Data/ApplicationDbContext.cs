using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smart.Core.Entities;

namespace Smart.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //  DbSet для кожної  сутності
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //налаштування усіх звязків між таблицями 

            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId);

            modelBuilder.Entity<WorkItem>()
                .HasOne(w => w.Project)
                .WithMany(p => p.WorkItems)
                .HasForeignKey(w => w.ProjectId);

            modelBuilder.Entity<WorkItem>()
                .HasOne(w => w.ParentTask)
                .WithMany(w => w.SubTasks)
                .HasForeignKey(w => w.ParentTaskId);

            modelBuilder.Entity<WorkItem>()
                .HasOne(w => w.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(w => w.AssignedUserId);

            modelBuilder.Entity<WorkItem>()
                .HasOne(w => w.CreatedByUser)
                .WithMany()
                .HasForeignKey(w => w.CreatedByUserId);

            modelBuilder.Entity<Chat>()
              .HasMany(c => c.ChatMessages)
              .WithOne(cm => cm.Chat)
              .HasForeignKey(cm => cm.ChatId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.Chat)
                .WithMany(c => c.ChatMessages)
                .HasForeignKey(cm => cm.ChatId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.ChatMessages)
                .HasForeignKey(cm => cm.UserId);

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.AttachedWorkItem)
                .WithMany(w => w.Attachments)
                .HasForeignKey(a => a.WorkItemId);

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.UploadedByUser)
                .WithMany(u => u.Attachments)
                .HasForeignKey(a => a.UploadedByUserId);



            modelBuilder.Entity<Comment>()
                .HasOne(c => c.WorkItem)
                .WithMany(w => w.Comments)
                .HasForeignKey(c => c.TaskId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.UserProjects)
                .WithOne(up => up.Project)
                .HasForeignKey(up => up.ProjectId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.WorkItems)
                .WithOne(w => w.Project)
                .HasForeignKey(w => w.ProjectId);


        }
    }
}

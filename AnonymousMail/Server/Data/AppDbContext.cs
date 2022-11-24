using AnonymousMail.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AnonymousMail.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<MailMessage> MailMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MailMessage>(entity =>
            {
                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.MailMessagesFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.MailMessagesToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            base.OnModelCreating(builder);
        }
    }
}

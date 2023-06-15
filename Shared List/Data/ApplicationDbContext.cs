using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared_List.Models;

namespace Shared_List.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Note> Lists { get; set; }
        public DbSet<UserList> UserLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserList>()
                .HasKey(ul => new { ul.ListId, ul.UserId });

            modelBuilder.Entity<UserList>()
                .HasOne(ul => ul.List)
                .WithMany(l => l.UserLists)
                .HasForeignKey(ul => ul.ListId);

            modelBuilder.Entity<UserList>()
                .HasOne(ul => ul.User)
                .WithMany()
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
        
}
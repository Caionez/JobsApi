using Microsoft.EntityFrameworkCore;

namespace Unisys_JobsApi.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasMany<Task>("Tasks")
                .WithOne();
        }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Task> Tasks { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Unisys_JobsApi.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        { }
        public DbSet<Models.Task> Tasks { get; set; }
    }
}
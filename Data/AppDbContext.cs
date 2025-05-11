using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;



namespace TaskManagementSystem.Data
{
    /// <summary>
    /// EF Core context that includes Identity tables and custom tables like TaskItem.
    /// </summary>
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options){}

        public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    }
}
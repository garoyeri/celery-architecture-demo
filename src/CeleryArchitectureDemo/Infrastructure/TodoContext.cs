using CeleryArchitectureDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace CeleryArchitectureDemo.Infrastructure
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
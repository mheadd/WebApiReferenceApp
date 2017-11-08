using Microsoft.EntityFrameworkCore;

namespace WebApiReferenceApp.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Person> People { get; set; }
    }
}
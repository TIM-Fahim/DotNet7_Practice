using Microsoft.EntityFrameworkCore;

namespace MinimalApiPractice
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Models.FormBody> FormBodies { get; set; }
    }
}

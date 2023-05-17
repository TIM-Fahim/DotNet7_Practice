using B2BCompany.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B2BCompany.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<CompanyConfig> CompanyConfigs { get; set; }

        public DbSet<ConfigValue> ConfigValues { get; set; }
    }
}
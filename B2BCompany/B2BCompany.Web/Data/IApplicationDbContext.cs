using B2BCompany.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BCompany.Web.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Company> Companys { get; set; }
        public DbSet<CompanyConfig> CompanyConfigs { get; set; }

        public DbSet<ConfigValue> ConfigValues { get; set; }
    }
}

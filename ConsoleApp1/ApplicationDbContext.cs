using System.Data.Entity;
using Calabonga.EntityFramework;

namespace Calabonga.Framework.Demo
{
    public class ApplicationDbContext : DbContext, IEntityFrameworkContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            LastSaveChangesResult = new SaveChangesResult();
        }
        public SaveChangesResult LastSaveChangesResult { get; }

        public DbSet<Person> People { get; set; }
    }
}
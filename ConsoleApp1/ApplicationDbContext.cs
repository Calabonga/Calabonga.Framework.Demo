using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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


        public override int SaveChanges()
        {
            try
            {
                var createdSourceInfo = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
                var modifiedSourceInfo = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

                foreach (var entry in createdSourceInfo)
                {
                    // do some staff
                    // ...

                    // Add system message
                    LastSaveChangesResult.AddMessage($"ChangeTracker has new entities: {entry.Entity.GetType()}");
                }

                foreach (var entry in modifiedSourceInfo)
                {
                    // do some staff
                    // ...

                    LastSaveChangesResult.AddMessage($"ChangeTracker has modified entities: {entry.Entity.GetType()}");
                }

                return base.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                LastSaveChangesResult.Exception = exception;
                return 0;
            }
        }
    }
}
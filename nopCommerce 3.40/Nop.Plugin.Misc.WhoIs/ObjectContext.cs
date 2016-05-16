using Nop.Core;
using Nop.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Nop.Plugin.Misc.WhoIs.Data
{
    public class WhoIsObjectContext : DbContext, IDbContext
    {
        public WhoIsObjectContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new WhoIsMap());

            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            Database.SetInitializer<WhoIsObjectContext>(null);
            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }

        public void Uninstall()
        {
            var dbScript = "DROP TABLE WhoIs";
            Database.ExecuteSqlCommand(dbScript);
            SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public System.Collections.Generic.IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

    }
}
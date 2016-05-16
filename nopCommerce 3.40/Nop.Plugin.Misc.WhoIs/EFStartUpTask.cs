using Nop.Core.Infrastructure;
using System.Data.Entity;

namespace Nop.Plugin.Misc.WhoIs.Data
{
    public class EfStartUpTask : IStartupTask
    {
        public void Execute()
        {
            Database.SetInitializer<WhoIsObjectContext>(null);
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
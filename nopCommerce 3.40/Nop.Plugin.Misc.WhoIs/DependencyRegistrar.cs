using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Misc.WhoIs.Data;
using Nop.Plugin.Misc.WhoIs.Domain;

namespace Nop.Plugin.Misc.WhoIs
{
    public class WhoIsDependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_misc_WhoIs";

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            var dataSettingsManager = new DataSettingsManager();
            DataSettings dataSettings = dataSettingsManager.LoadSettings();
            builder.Register<IDbContext>(c => RegisterIDbContext(c, dataSettings)).Named<IDbContext>(CONTEXT_NAME).InstancePerLifetimeScope();
            builder.Register(c => RegisterIDbContext(c, dataSettings)).InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<WhoIsRecord>>().As<IRepository<WhoIsRecord>>().WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME)).InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 0; }
        }

        private WhoIsObjectContext RegisterIDbContext(IComponentContext componentContext, DataSettings dataSettings)
        {
            string dataConnectionStrings;

            if (dataSettings != null && dataSettings.IsValid())
            {
                dataConnectionStrings = dataSettings.DataConnectionString;
            }
            else
            {
                dataConnectionStrings = componentContext.Resolve<DataSettings>().DataConnectionString;
            }

            return new WhoIsObjectContext(dataConnectionStrings);
        }
    }
}

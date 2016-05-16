using Nop.Core.Domain.Logging;
using Nop.Core.Plugins;
using Nop.Plugin.Misc.WhoIs.Data;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;

namespace Nop.Plugin.Misc.WhoIs
{
    class WhoIsPlugin : BasePlugin
    {
        #region Constructors

        private readonly WhoIsObjectContext _context;
        private readonly ILogger _logger;

        public WhoIsPlugin(WhoIsObjectContext context, ILocalizationService localizationService, IPermissionService permissionService, ISettingService settingService, ILogger logger)
        {
            _context = context;
            this._logger = logger;
        }

        #endregion

        public override void Install()
        {
            //install database
            _context.Install();

            base.Install();

            _logger.InsertLog(LogLevel.Information, "The WhoIs api plugin has been installed successfully.");
        }

        public override void Uninstall()
        {
            _context.Uninstall();

            base.Uninstall();

            _logger.InsertLog(LogLevel.Information, "The WhoIs api plugin has been uninstalled successfully.");
        }

    }
}

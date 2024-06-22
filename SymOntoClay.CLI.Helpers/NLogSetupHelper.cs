using NLog;
using System.Reflection;

namespace SymOntoClay.CLI.Helpers
{
    public static class NLogSetupHelper
    {
        public static void UseAppConfig()
        {
            var appConfigFileName = GetAppConfigFileName();

            if(string.IsNullOrWhiteSpace(appConfigFileName))
            {
                return;
            }

            var config = new NLog.Config.XmlLoggingConfiguration(appConfigFileName);

            LogManager.Configuration = config;
        }

        private static string GetAppConfigFileName()
        {
            var applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            var entryAssemblyName = Assembly.GetEntryAssembly().GetName().Name;

            var exeConfigFileName = $"{entryAssemblyName}.exe.config";

            var exeConfigFullFileName = Path.Combine(applicationBase, exeConfigFileName);

            if(File.Exists(exeConfigFullFileName))
            {
                return exeConfigFullFileName;
            }

            var dllConfigFileName = $"{entryAssemblyName}.dll.config";

            var dllConfigFullFileName = Path.Combine(applicationBase, dllConfigFileName);

            if(File.Exists(dllConfigFullFileName))
            {
                return dllConfigFileName;
            }

            return string.Empty;
        }
    }
}

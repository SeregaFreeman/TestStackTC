using log4net;
using log4net.Config;

namespace TestStackFramework.utils
{
    public static class LoggerUtil
    {
        private static ILog log = LogManager.GetLogger("LOGGER");

        public static ILog Log => log;

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }

        public static void Info(string message)
        {
            Log.Info(message);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }
    }
}

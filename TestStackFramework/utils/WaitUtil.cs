using System;
using System.Configuration;
using TestStack.White.Utility;

namespace TestStackFramework.utils
{
    public class WaitUtil
    {
        private static readonly TimeSpan RetryFor = TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"]));
        private static readonly TimeSpan RetryInterval = TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultRetryInterval"]));

        public static bool WaitForCondition(Func<bool> getMethod, TimeSpan? retryFor = null, TimeSpan? retryInterval = null)
        {
            retryFor = retryFor ?? RetryFor;
            retryInterval = retryInterval ?? RetryInterval;

            return Retry.For(getMethod, g => !g, (TimeSpan) retryFor, retryInterval);
        }
    }
}

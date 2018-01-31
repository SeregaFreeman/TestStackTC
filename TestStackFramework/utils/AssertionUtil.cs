using NUnit.Framework;

namespace TestStackFramework.utils
{
    public class AssertionUtil
    {
        public static void AssertTrue(bool condition, string message)
        {
            try
            {
                Assert.True(condition, message);
                LoggerUtil.Info("Condition is true");
            }
            catch (AssertionException ex)
            {
                LoggerUtil.Error($"Expected 'true', found {condition}" + ex);
                Assert.Fail();
            }
        }
    }
}

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
            }
            catch (AssertionException ex)
            {
                LoggerUtil.Error($"Expected 'true', found {condition}" + ex);
                Assert.Fail();
            }
        }

        public static void AssertFalse(bool condition, string message)
        {
            try
            {
                Assert.False(condition, message);
            }
            catch (AssertionException ex)
            {
                LoggerUtil.Error($"Expected 'false', found {condition}" + ex);
                Assert.Fail();
            }
        }

        public static void AssertNotNull(object targetObject, string message)
        {
            try
            {
                Assert.NotNull(targetObject, message);
            }
            catch (AssertionException ex)
            {
                LoggerUtil.Error($"Expected {targetObject} to be not null, but get" + ex);
                Assert.Fail();
            }
        }

        public static void AssertEquals(object expected, object actual, string message)
        {
            try
            {
                Assert.AreEqual(expected, actual, message);
            }
            catch (AssertionException ex)
            {
                LoggerUtil.Error($"Expected objects to be equal, but get" + ex);
                Assert.Fail();
            }
        }
    }
}

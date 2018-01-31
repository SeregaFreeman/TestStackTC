using TestStackFramework.framework;
using TechTalk.SpecFlow;
using TestStackFramework.utils;

namespace Tests.hooks
{
    [Binding]
    public class BeforeAfterScenario
    {
        [BeforeScenario]
        public void InitLogger()
        {
            LoggerUtil.InitLogger();
            LoggerUtil.Info("Starting the scenario...");
        }

        [AfterScenario]
        public void CloseApp()
        {
            Scope.Application.Kill();
        }
    }
}
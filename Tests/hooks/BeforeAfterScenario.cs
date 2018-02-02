using System.Configuration;
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
            ProcessesUtil.CloseAllProcessesByName(ConfigurationManager.AppSettings["processName"]);
        }

        [AfterScenario]
        public void CloseApp()
        {
            FileUtil.DeleteFile((string)ScenarioContext.Current["folder1"], (string)ScenarioContext.Current["filename1"]);
            FileUtil.DeleteFile((string)ScenarioContext.Current["folder2"], (string)ScenarioContext.Current["filename2"]);
            Scope.Application.Kill();
        }
    }
}
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using TestStackFramework.framework;
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
            var foldersCount = (int) ScenarioContext.Current["folders count"];
            LoggerUtil.Info($"Deleting {foldersCount} folders...");
            for (var i = 0; i < foldersCount; i++)
            {
                FileUtil.DeleteDirectory((string)ScenarioContext.Current[$"folder_{i}"], true);
            }
            
            Scope.Application.Kill();
            LoggerUtil.Info($"Scenario is finished. {Environment.NewLine}");
        }
    }
}
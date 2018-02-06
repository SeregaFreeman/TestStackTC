﻿using System;
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
            FileUtil.DeleteFile((string)ScenarioContext.Current["folder1"], (string)ScenarioContext.Current["filename1"]);
            FileUtil.DeleteFile((string)ScenarioContext.Current["folder2"], (string)ScenarioContext.Current["filename2"]);
            Scope.Application.Kill();
            LoggerUtil.Info($"Scenario is finished. {Environment.NewLine}");
        }
    }
}
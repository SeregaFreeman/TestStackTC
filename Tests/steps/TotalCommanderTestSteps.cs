using System;
using System.Collections.Generic;
using System.Windows;
using System.Configuration;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.Finders;
using TestStackFramework.framework;
using TestStackFramework.utils;
using Views;
using System.Windows.Automation;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.Utility;
using TestStack.White.WindowsAPI;
using TestStackFramework.framework.elements;

namespace Tests.steps
{
    [Binding]
    public class TotalCommanderTestSteps
    {
        [Given(@"Folders ""(.*)"", ""(.*)"" with files ""(.*)"", ""(.*)""  were created if was needed")]
        public void CreateFoldersWithFiles(string folder1, string folder2, string filename1, string filename2)
        {
            ScenarioContext.Current.Add("folder1", folder1);
            ScenarioContext.Current.Add("folder2", folder2);
            ScenarioContext.Current.Add("filename1", filename1);
            ScenarioContext.Current.Add("filename2", filename2);

            FileUtil.CreateFolder(folder1);
            FileUtil.CreateFolder(folder2);
            FileUtil.CreateFile(folder1, filename1);
            FileUtil.CreateFile(folder2, filename2);
        }

        [When(@"User opens the app")]
        public void StartApp()
        {
            Scope.Application = MyApp.Launch(ConfigurationManager.AppSettings["Path"], ConfigurationManager.AppSettings["EXE"]);
            Scope.DefaultWindow = MyApp.Window;
        }

        [Then(@"Trial version window is open")]
        public void ThenTrialVersionWindowIsOpen()
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindows().FirstOrDefault();
            //TNASTYNAGSCREEN
            AssertionUtil.AssertTrue(Scope.DefaultWindow.Title == ConfigurationManager.AppSettings["ModalWindowName"], "Modal window is wrong");
        }

        [When(@"User clicks button with proper number to access app")]
        public void ClickButtonWithProperNumberToAccessApp()
        {
            StartView.ButtonToStartUsingApp(StartView.PanelWelcome.GetText()).Click();
        }

        [Then(@"Main window is open")]
        public void ThenMainWindowIsOpen()
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindows().FirstOrDefault();
            AssertionUtil.AssertTrue(Scope.DefaultWindow.Title == ConfigurationManager.AppSettings["MainWindowName"], "Main window is wrong");
        }

        [When(@"User opens folder ""(.*)"" in ""(.*)"" panel")]
        public void WhenUserOpensFolderInPanel(string folder, string panel)
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindow(ConfigurationManager.AppSettings["MainWindowName"]);
            MainView.ListBoxPanel(GetPanelIndex(panel)).Click();
            Thread.Sleep(2000);
            OpenPath(panel, folder);
            Thread.Sleep(2000);
        }

        [Then(@"folder ""(.*)"" is open in ""(.*)"" panel")]
        public void ThenFolderIsOpenInPanel(string p0, string p1)
        {
            
        }

        public void OpenPath(string panel, string folder)
        {
            Thread.Sleep(2000);
            var textlines = MainView.Panels;
            List<Panel> panels = new List<Panel>();
            panels.AddRange(textlines.Where(textline => textline.Name.Contains("*.*")));
            panels[GetPanelIndex(panel)].Click();
            Thread.Sleep(2000);
            MainView.PathTextBox(panels[GetPanelIndex(panel)].RawItem).SetValue(folder);
            MainView.PathTextBox(panels[GetPanelIndex(panel)].RawItem).KeyIn(KeyboardInput.SpecialKeys.RETURN);
        }

        [When(@"User moves ""(.*)"" from ""(.*)"" panel to ""(.*)""")]
        public void WhenUserMovesFromPanelTo(string filename, string fromPanel, string toPanel)
        {
            MoveFile(GetPanelIndex(fromPanel), filename, GetPanelIndex(toPanel));
        }

        [Then(@"Confirmation window is open")]
        public void ThenConfirmationWindowIsOpen()
        {
            //TInpComboDlg
            Scope.DefaultWindow = Scope.Application.Application.GetWindows().FirstOrDefault();
            LoggerUtil.Info(Scope.DefaultWindow.Name);
        }

        [When(@"User confirms file movement")]
        public void WhenUserConfirmsFileMovement()
        {
            MoveConfirmationView.ButtonConfirmMovement.Click();
            Thread.Sleep(2000);
        }

        [Then(@"File ""(.*)"" is moved to folder ""(.*)"" on ""(.*)"" panel")]
        public void ThenFileIsMovedToFolderOnPanel(string p0, string p1, string p2)
        {
            
        }

        [When(@"User selects ""(.*)"" option from context menu for ""(.*)"" on ""(.*)"" panel")]
        public void WhenUserSelectsOptionFromContextMenuForOnPanel(string option, string filename, string panel)
        {
            SelectItemInContextMenu(GetPanelIndex(panel), filename, option);
        }

        [When(@"User selects ""(.*)"" option from context menu on ""(.*)"" panel")]
        public void WhenUserSelectsOptionFromContextMenuForPanel(string option, string panel)
        {
            SelectItemInContextMenu(GetPanelIndex(panel), option);
        }

        [Then(@"Replace or skip files window is open")]
        public void ThenReplaceOrSkipFilesWindowIsOpen()
        {
            Retry.For(() => MyApp.IsWindowOpen("Replace or Skip Files"), TimeSpan.FromSeconds(5));
        }

        [When(@"User clicks ""(.*)"" button on dialog window")]
        public void WhenUserClicksButtonOnDialogWindow(string option)
        {
            switch (option)
            {
                case "Replace":
                    ReplaceOrSkipFilesView.ButtonReplace.Click();
                    break;
                case "Skip":
                    ReplaceOrSkipFilesView.ButtonSkip.Click();
                    break;
            }
        }

        [Then(@"""(.*)"" is present on ""(.*)"" panel")]
        public void ThenIsPresentOnPanel(string filename, string panel)
        {
            AssertionUtil.AssertTrue(IsFileFound(panel, filename), "File is not present when should be");
        }

        [Then(@"""(.*)"" is absent on ""(.*)"" panel")]
        public void ThenIsAbsentOnPanel(string filename, string panel)
        {
            AssertionUtil.AssertFalse(IsFileFound(panel, filename), "File is present when should not be");
        }

        [When(@"User selects ""(.*)"", ""(.*)"", ""(.*)"" from the main app menu")]
        public void WhenUserSelectsFromTheMainAppMenu(string firstLevel, string secondLevel, string thirdLevel)
        {
            MainView.MenuBarApplication.SelectMenu(firstLevel, secondLevel, thirdLevel);
        }

        [Then(@"Side panel is open")]
        public void ThenSidePanelIsOpen()
        {
            AssertionUtil.AssertTrue(MainView.ListBoxSidePanel.IsVisible(), "Side panel is not visible");
        }

        [When(@"User clicks on ""(.*)"" menu item")]
        public void WhenUserClicksOnMenuItem(string itemName)
        {
            if (itemName == "Switch through tree panel options")
            {
                SikuliUtil.Click(ConfigurationManager.AppSettings["sikuliImagesPath"], "switchThrough.png", 0.8f);
            }
        }



        public void OpenFolder(int panelId, string folderName)
        {
            foreach (var listBoxItem in MainView.ListBoxPanel(panelId).GetItems())
            {
                if (!listBoxItem.Name.Contains(folderName)) continue;
                LoggerUtil.Info($"List Item {folderName} is found, double clicking");
                listBoxItem.DoubleClick();
                break;
            }
        }

        public void MoveFile(int fromPanel, string filename, int toPanel)
        {

            Point fileCoordinates = new Point();
            var item = MainView.ListBoxPanel(fromPanel).GetItems();
            foreach (var listBoxItem in item)
            {
                if (!listBoxItem.Name.Contains(filename)) continue;
                LoggerUtil.Info($"List Item {filename} is found, getting clickable point");
                listBoxItem.Click();
                fileCoordinates = new Point(listBoxItem.Bounds.BottomRight.X - 2,
                    listBoxItem.Bounds.BottomRight.Y - 2);
                break;
            }

            LoggerUtil.Info("Performing drag and drop");
            Mouse.Instance.Location = fileCoordinates;
            Mouse.LeftDown();
            Thread.Sleep(1000);
            Mouse.Instance.Location =
                new Point(MainView.ListBoxPanel(toPanel).GetBounds().BottomRight.X - 10, 
                          MainView.ListBoxPanel(toPanel).GetBounds().BottomRight.Y - 10);
            Thread.Sleep(1000);
            Mouse.LeftUp();
        }

        public void SelectItemInContextMenu(int fromPanel, string itemName, string property)
        {
            foreach (var listBoxItem in MainView.ListBoxPanel(fromPanel).GetItems())
            {
                if (!listBoxItem.Name.Contains(itemName)) continue;
                LoggerUtil.Info($"List Item {itemName} is found, right clicking");
                listBoxItem.RightClick();
                LoggerUtil.Log.Info($"Selecting property {property}");
                Scope.DefaultWindow.Popup.Item(property).Click();
                break;
            }
        }

        public void SelectItemInContextMenu(int panel, string property)
        {
            LoggerUtil.Info($"Selecting property {property} in context menu");
            Mouse.Instance.Location = 
                new Point(MainView.ListBoxPanel(panel).GetBounds().BottomLeft.X + 20,
                          MainView.ListBoxPanel(panel).GetBounds().BottomLeft.Y - 10);
            Mouse.Instance.RightClick();
            Scope.DefaultWindow.Popup.Item(property).Click();
        }

        public int GetPanelIndex(string panelName)
        {
            switch (panelName)
            {
                case "left":
                    return 0;
                case "right":
                    return 1;
                default:
                    throw new Exception("Incorrect panel name");
            }
        }

        public bool IsFileFound(string panel, string file)
        {
            Retry.For(() => MyApp.IsWindowOpen(ConfigurationManager.AppSettings["MainWindowName"]), TimeSpan.FromSeconds(5));
            Scope.DefaultWindow.Focus();
            Retry.For(() => Scope.DefaultWindow.IsFocussed, TimeSpan.FromSeconds(5));
            bool loaded = Retry.For(() => MainView.ListBoxPanel(GetPanelIndex(panel)).GetItems().Exists(item => item.Name.Contains(file)), TimeSpan.FromSeconds(5));
            LoggerUtil.Log.Info($"Finding file {file} on {panel} panel");
            return loaded;
        }
    }
}

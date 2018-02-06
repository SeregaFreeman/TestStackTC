using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Windows;
using TechTalk.SpecFlow;
using TestStack.White.InputDevices;
using TestStack.White.Utility;
using TestStack.White.WindowsAPI;
using TestStackFramework.framework;
using TestStackFramework.framework.elements;
using TestStackFramework.utils;
using Views;

namespace Tests.steps
{
    [Binding]
    public class TotalCommanderTestSteps
    {
        [Given(@"Folders ""(.*)"", ""(.*)"" with files ""(.*)"", ""(.*)""  were created if was needed")]
        public void CreateFoldersWithFiles(string folder1, string folder2, string filename1, string filename2)
        {
            FileUtil.CreateFolder(folder1);
            FileUtil.CreateFolder(folder2);
            FileUtil.CreateFile(folder1, filename1);
            FileUtil.CreateFile(folder2, filename2);

            ScenarioContext.Current.Add("folder1", folder1);
            ScenarioContext.Current.Add("folder2", folder2);
            ScenarioContext.Current.Add("filename1", filename1);
            ScenarioContext.Current.Add("filename2", filename2);
        }

        [When(@"User opens the app")]
        public void StartApp()
        {
            Scope.Application = MyApp.Launch(ConfigurationManager.AppSettings["Path"], ConfigurationManager.AppSettings["EXE"]);
            Scope.DefaultWindow = MyApp.Window;
        }

        [Then(@"Trial version window is open")]
        public void AssertTrialVersionWindowIsOpen()
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindows().FirstOrDefault();
            AssertionUtil.AssertTrue(Scope.DefaultWindow.Title == ConfigurationManager.AppSettings["ModalWindowName"], "Modal window is wrong");
        }

        [When(@"User clicks button with proper number to access app")]
        public void ClickButtonWithProperNumberToAccessApp()
        {
            StartView.ButtonToStartUsingApp(StartView.PanelWelcome.GetText()).Click();
        }

        [Then(@"Main window is open")]
        public void AssertMainWindowIsOpen()
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindows().FirstOrDefault();
            AssertionUtil.AssertTrue(Scope.DefaultWindow.Title == ConfigurationManager.AppSettings["MainWindowName"], "Main window is wrong");
        }

        [When(@"User opens folder ""(.*)"" in ""(.*)"" panel")]
        public void OpenFolderInPanelByPath(string path, string panel)
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindow(ConfigurationManager.AppSettings["MainWindowName"]);
            MainView.ListBoxPanel(GetPanelIndex(panel)).Click();
            Thread.Sleep(2000);
            OpenPath(panel, path);
        }

        [Then(@"folder ""(.*)"" is open in ""(.*)"" panel")]
        public void AssertFolderIsOpenInPanel(string folder, string panel)
        {
            MainView.ListBoxPanel(GetPanelIndex(panel)).Click();
            AssertionUtil.AssertNotNull(MainView.PanelCurrentDirectory(folder), "Current panel directory is incorrect");
        }

        [When(@"User moves ""(.*)"" from ""(.*)"" panel to ""(.*)""")]
        public void MoveFileFromOnePanelToAnother(string filename, string fromPanel, string toPanel)
        {
            MoveFile(GetPanelIndex(fromPanel), filename, GetPanelIndex(toPanel));
        }

        [Then(@"Confirmation window is open")]
        public void ThenConfirmationWindowIsOpen()
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindows().FirstOrDefault();
            AssertionUtil.AssertTrue(Scope.DefaultWindow.ModalWindow(ConfigurationManager.AppSettings["ModalWindowName"]).IsCurrentlyActive, 
                "Modal window is not active");
        }

        [When(@"User confirms file movement")]
        public void ConfirmFileMovement()
        {
            MoveConfirmationView.ButtonConfirmMovement.Click();
        }

        [When(@"User selects ""(.*)"" option from context menu for ""(.*)"" on ""(.*)"" panel")]
        public void SelectOptionFromContextMenuForItemOnPanel(string option, string filename, string panel)
        {
            SelectItemInContextMenu(GetPanelIndex(panel), filename, option);
        }

        [When(@"User selects ""(.*)"" option from context menu on ""(.*)"" panel")]
        public void SelectOptionFromContextMenuOnPanel(string option, string panel)
        {
            SelectItemInContextMenu(GetPanelIndex(panel), option);
        }

        [Then(@"Replace or skip files window is open")]
        public void ReplaceOrSkipFilesWindowIsOpen()
        {
            Retry.For(() => MyApp.IsWindowOpen("Replace or Skip Files"), TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"])));
        }

        [When(@"User clicks ""(.*)"" button on dialog window")]
        public void ClickButtonOnDialogWindow(string option)
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
        public void AssertItemIsPresentOnPanel(string filename, string panel)
        {
            AssertionUtil.AssertTrue(IsFileFound(panel, filename), "File is not present when should be");
        }

        [Then(@"""(.*)"" is absent on ""(.*)"" panel")]
        public void AssertItemIsAbsentOnPanel(string filename, string panel)
        {
            AssertionUtil.AssertFalse(IsFileFound(panel, filename), "File is present when should not be");
        }

        [When(@"User selects ""(.*)"" => ""(.*)"" => ""(.*)"" from the main app menu")]
        public void SelectItemFromTheMainAppMenu(string firstLevel, string secondLevel, string thirdLevel)
        {
            MainView.MenuBarApplication.SelectMenu(firstLevel, secondLevel, thirdLevel);
        }

        [When(@"User selects ""(.*)"", ""(.*)"" from the main app menu")]
        public void SelectItemFromTheMainAppMenu(string firstLevel, string secondLevel)
        {
            MainView.MenuBarApplication.SelectMenu(firstLevel, secondLevel);
        }

        [Then(@"Side panel is open")]
        public void AssertSidePanelIsOpen()
        {
            AssertionUtil.AssertTrue(MainView.ListBoxSidePanel.IsVisible(), "Side panel is not visible");
        }

        [Then(@"Side panel is not open")]
        public void AssertSidePanelIsNotOpen()
        {
            AssertionUtil.AssertFalse(MainView.ListBoxSidePanel.IsVisible(), "Side panel is still visible");
        }

        [When(@"User makes ""(.*)"" panel active")]
        public void MakePanelActive(string panel)
        {
            MainView.ListBoxPanel(GetPanelIndex(panel)).Click();
        }

        [When(@"User clicks ""(.*)"" times on ""(.*)"" icon")]
        public void ClickOnIconUsingSikuli(int clicksCount, string itemName)
        {
            string image = null;
            switch (itemName)
            {
                case "Switch through tree panel options":
                    image = "switchThrough.png";
                    break;

                case "Search":
                    image = "find.png";
                    break;
            }

            for (var i = 0; i < clicksCount; i++)
            {
                SikuliUtil.Click(ConfigurationManager.AppSettings["sikuliImagesPath"], image, 0.8f);
            }
        }

        [Then(@"""(.*)"" tab item is selected")]
        public void AssertTabItemIsSelected(string tabItem)
        {
            AssertionUtil.AssertEquals(FindFilesView.TabFindFiles.GetSelectedTabName(), tabItem, "tabs are different");
        }

        [Then(@"""(.*)"" field value is ""(.*)""")]
        public void AssertFieldValueIsCorrect(string field, string value)
        {
            switch (field)
            {
                case "Search in":
                    AssertionUtil.AssertEquals(value, FindFilesView.TextBoxSearchIn.Name, "Search in field value is incorrect");
                    break;
            }
        }

        [When(@"User sets value ""(.*)"" to ""(.*)"" field")]
        public void SetValueToField(string value, string field)
        {
            switch (field)
            {
                case "Search for":
                    FindFilesView.TextBoxSearchFor.BulkText(value);
                    break;
            }
        }

        [When(@"User checks ReGex checkbox")]
        public void CheckReGexCheckbox()
        {
            FindFilesView.CheckBoxRegEx.Click();
        }

        [When(@"User clicks Start search button")]
        public void ClickStartSearchButton()
        {
            MainView.ButtonStartSearch.Click();
        }

        [Then(@"Only ""(.*)"" is found")]
        public void AssertThenOnlyItemIsFound(string filename)
        {
            AssertionUtil.AssertEquals(FindFilesView.ListBoxSearchResults.GetItems().Count, 2, "Results list size is incorrect");
            AssertionUtil.AssertNotNull(FindFilesView.ListItemSearchResultsCount(1, 0), "Count is incorrect");
            AssertionUtil.AssertNotNull(FindFilesView.ListItemSearchResult(filename), "Found item is incorrect");
        }

        [When(@"User closes search window")]
        public void CloseCurrentWindow()
        {
            FindFilesView.ButtonCloseWindow.Click();
        }

        [Then(@"Search window is closed")]
        public void AssertWindowIsClosed()
        {
            AssertionUtil.AssertTrue(FindFilesView.FindFilesWindow.IsClosed, "window is not closed");
        }

        [When(@"User unselects all files on ""(.*)"" panel")]
        public void UnselectAllFiles(string panel)
        {
            SelectItemOnPanel("..", panel);
        }

        [Then(@"Warning window is open")]
        public void AssertWarningWindowIsOpen()
        {
            AssertionUtil.AssertTrue(Scope.DefaultWindow.ModalWindow(ConfigurationManager.AppSettings["ModalWindowName"]).IsCurrentlyActive,
                "Modal window is not found");
            AssertionUtil.AssertNotNull(MainView.LabelNoFilesSelected(
                Scope.DefaultWindow.ModalWindow(ConfigurationManager.AppSettings["ModalWindowName"])),
                "No label found");
        }

        [When(@"User closes warning window")]
        public void CloseWarningWindow()
        {
            MainView.ButtonCloseEditCommentDialog.Click();
        }

        [Then(@"Warning window is closed")]
        public void AssertWarningWindowIsClosed()
        {
            List<string> titles = new List<string>();
            foreach (var modal in Scope.DefaultWindow.ModalWindows())
            {
                titles.Add(modal.Title);
            }

            AssertionUtil.AssertFalse(titles.Contains(ConfigurationManager.AppSettings["ModalWindowName"]), 
                "Modal window is found when should not");
        }

        [Then(@"App is closed")]
        public void AssertAppIsClosed()
        {
            Retry.For(() => Scope.Application.Application.HasExited, TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"])));
            AssertionUtil.AssertTrue(Scope.Application.Application.HasExited, "App is not stopped yet");
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
            MouseInteractionUtil.DragAndDropByLocation(fileCoordinates, new Point(
                MainView.ListBoxPanel(toPanel).GetBounds().BottomRight.X - 10,
                MainView.ListBoxPanel(toPanel).GetBounds().BottomRight.Y - 10));
        }

        public void SelectItemInContextMenu(int fromPanel, string itemName, string property)
        {
            foreach (var listBoxItem in MainView.ListBoxPanel(fromPanel).GetItems())
            {
                if (!listBoxItem.Name.Contains(itemName)) continue;
                LoggerUtil.Info($"List Item {itemName} is found, right clicking");
                Mouse.Instance.Location = new Point(listBoxItem.Bounds.X, listBoxItem.Bounds.Y);
                MouseInteractionUtil.RightClickWithDelay();
                LoggerUtil.Info($"Selecting property {property}");
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
            MouseInteractionUtil.RightClickWithDelay();
            Scope.DefaultWindow.Popup.Item(property).Click();
        }

        public void SelectItemOnPanel(string itemName, string panel)
        {
            foreach (var panelItem in MainView.ListBoxPanel(GetPanelIndex(panel)).GetItems())
            {
                if (!panelItem.Name.Contains(itemName)) continue;
                LoggerUtil.Info($"Item {itemName} is found on {panel} panel");
                panelItem.Click();
            }
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
            Retry.For(() => MyApp.IsWindowOpen(ConfigurationManager.AppSettings["MainWindowName"]), TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"])));
            Scope.DefaultWindow.Focus();
            Retry.For(() => Scope.DefaultWindow.IsFocussed, TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"])));
            var loaded = Retry.For(() => MainView.ListBoxPanel(GetPanelIndex(panel)).GetItems().Exists(item => item.Name.Contains(file)), TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"])));
            LoggerUtil.Log.Info($"Finding file {file} on {panel} panel");
            return loaded;
        }
    }
}
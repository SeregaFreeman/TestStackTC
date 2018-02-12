using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using TechTalk.SpecFlow;
using Tests.businessLogic;
using TestStackFramework.framework;
using TestStackFramework.utils;
using Views;

namespace Tests.steps
{
    [Binding]
    public class TotalCommanderTestSteps
    {
        [Given(@"The following folders were created if were absent:")]
        public void CreateFolders(Table table)
        {
            ScenarioContext.Current.Add("folders count", table.Rows.Count);
            for (var i = 0; i < table.Rows.Count; i++)
            {
                FileUtil.CreateDirectory(table.Rows[i]["Folder path"]);
                ScenarioContext.Current.Add($"folder_{i}", table.Rows[i]["Folder path"]);
            }
        }

        [Given(@"The following files were created in corresponding folders:")]
        public void CreateFiles(Table table)
        {
            foreach (var row in table.Rows)
            {
                FileUtil.CreateFile(row["Folder path"], row["File name"]);
            }
        }

        [When(@"User opens the app")]
        public void StartApp()
        {
            Scope.Application = MyApp.Launch(ConfigurationManager.AppSettings["Path"], ConfigurationManager.AppSettings["EXE"]);
            Scope.DefaultWindow = MyApp.Window;
        }

        [Then(@"""(.*)"" window is opened")]
        public void AssertTrialVersionWindowIsOpen(string windowName)
        {
            string window;
            switch (windowName)
            {
                case "Main":
                    window = ConfigurationManager.AppSettings["MainWindowName"];
                    break;

                case "Trial version welcome":
                    window = ConfigurationManager.AppSettings["ModalWindowName"];
                    break;

                default:
                    window = windowName;
                    break;
            }
            var isWindowOpen = WaitUtil.WaitForCondition(() => WindowBl.IsDefaultWindowOpen(window));
            AssertionUtil.AssertTrue(isWindowOpen, $"{windowName} is not open");
        }

        [Then(@"Confirmation window is active")]
        public void ThenConfirmationWindowIsOpen()
        {
            AssertionUtil.AssertTrue(WindowBl.IsDefaultWindowCurrentlyActive(
                ConfigurationManager.AppSettings["ModalWindowName"]), "Modal window is not active");
        }

        [When(@"User closes search window")]
        public void CloseSearchWindow()
        {
            FindFilesView.ButtonCloseWindow.Click();
        }

        [Then(@"Search window is closed")]
        public void AssertSearchWindowIsClosed()
        {
            AssertionUtil.AssertTrue(FindFilesView.FindFilesWindow.IsClosed, "Search window is not closed");
        }

        [Then(@"Warning window is active")]
        public void AssertWarningWindowIsOpen()
        {
            AssertionUtil.AssertTrue(WindowBl.IsDefaultWindowCurrentlyActive(ConfigurationManager.AppSettings["ModalWindowName"]), "Modal window is not active");
            AssertionUtil.AssertNotNull(MainView.LabelNoFilesSelected(
                    Scope.DefaultWindow.ModalWindow(ConfigurationManager.AppSettings["ModalWindowName"])), "No label found");
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

        [When(@"User clicks button with proper number to access app")]
        public void ClickButtonWithProperNumberToAccessApp()
        {
            StartView.ButtonToStartUsingApp(StartView.PanelWelcome.GetText()).Click();
        }

        [When(@"User opens following folders on corresponding panels:")]
        public void OpenFolderInPanelByPath(Table table)
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindow(ConfigurationManager.AppSettings["MainWindowName"]);
            foreach (var row in table.Rows)
            {
                MainView.ListBoxPanel(PanelBl.GetPanelIndex(row["Panel"])).Click();
                Thread.Sleep(2000);
                PanelBl.OpenPath(row["Panel"], row["Folder path"]);
            }
        }

        [Then(@"Following folders are open on corresponding panels:")]
        public void AssertFolderIsOpenInPanel(Table table)
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindow(ConfigurationManager.AppSettings["MainWindowName"]);
            foreach (var row in table.Rows)
            {
                var folderPath = row["Folder path"];
                var panel = row["Panel"];

                MainView.ListBoxPanel(PanelBl.GetPanelIndex(panel)).Click();
                AssertionUtil.AssertNotNull(MainView.PanelCurrentDirectory(folderPath), "Current panel directory is incorrect");
            }
        }

        [When(@"User moves ""(.*)"" from ""(.*)"" panel to ""(.*)"" using drag and drop")]
        public void MoveFileFromOnePanelToAnother(string filename, string fromPanel, string toPanel)
        {
            PanelBl.MoveFile(PanelBl.GetPanelIndex(fromPanel), filename, PanelBl.GetPanelIndex(toPanel));
        }

        [When(@"User confirms file movement")]
        public void ConfirmFileMovement()
        {
            MoveConfirmationView.ButtonConfirmMovement.Click();
        }

        [When(@"User selects ""(.*)"" option from context menu for ""(.*)"" on ""(.*)"" panel")]
        public void SelectOptionFromContextMenuForItemOnPanel(string option, string filename, string panel)
        {
            PanelBl.SelectItemInContextMenu(PanelBl.GetPanelIndex(panel), filename, option);
        }

        [When(@"User selects ""(.*)"" option from context menu on ""(.*)"" panel")]
        public void SelectOptionFromContextMenuOnPanel(string option, string panel)
        {
            PanelBl.SelectItemInContextMenu(PanelBl.GetPanelIndex(panel), option);
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
            AssertionUtil.AssertTrue(PanelBl.IsFileFound(panel, filename), "File is not present when should be");
        }

        [Then(@"""(.*)"" is absent on ""(.*)"" panel")]
        public void AssertItemIsAbsentOnPanel(string filename, string panel)
        {
            AssertionUtil.AssertFalse(PanelBl.IsFileFound(panel, filename), "File is present when should not be");
        }

        [When(@"User selects ""(.*)"" from the main app menu")]
        public void SelectItemFromTheMainAppMenu(string path)
        {
            MainView.MenuBarApplication.SelectMenu(path.Split('/'));
        }

        [Then(@"Side panel is opened")]
        public void AssertSidePanelIsOpen()
        {
            AssertionUtil.AssertTrue(MainView.ListBoxSidePanel.IsVisible(), "Side panel is not visible");
        }

        [Then(@"Side panel is not opened")]
        public void AssertSidePanelIsNotOpen()
        {
            AssertionUtil.AssertFalse(MainView.ListBoxSidePanel.IsVisible(), "Side panel is still visible");
        }

        [When(@"User makes ""(.*)"" panel active")]
        public void MakePanelActive(string panel)
        {
            MainView.ListBoxPanel(PanelBl.GetPanelIndex(panel)).Click();
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
            AssertionUtil.AssertEquals(FindFilesView.TabFindFiles.SelectedTab.Name, tabItem, "Incorrect tab is selected");
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
            AssertionUtil.AssertEquals(FindFilesView.ListBoxSearchResults.Items.Count, 2, "Results list size is incorrect");
            AssertionUtil.AssertNotNull(FindFilesView.ListItemSearchResultsCount(1, 0), "Count is incorrect");
            AssertionUtil.AssertNotNull(FindFilesView.ListItemSearchResult(filename), "Found item is incorrect");
        }

        [When(@"User unselects all files on ""(.*)"" panel")]
        public void UnselectAllFiles(string panel)
        {
            PanelBl.SelectItemOnPanel("..", panel);
        }

        [When(@"User closes warning dialog")]
        public void CloseWarningDialog()
        {
            MainView.ButtonCloseEditCommentDialog.Click();
        }

        [Then(@"App is closed")]
        public void AssertAppIsClosed()
        {
            var hasExited = WaitUtil.WaitForCondition(() => Scope.Application.Application.HasExited);
            AssertionUtil.AssertTrue(hasExited, "App is not stopped yet");
        }
    }
}
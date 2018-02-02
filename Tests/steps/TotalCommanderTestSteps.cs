using System;
using System.Windows;
using System.Configuration;
using System.Threading;
using TechTalk.SpecFlow;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.Finders;
using TestStackFramework.framework;
using TestStackFramework.utils;
using Views;
using System.Windows.Automation;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.WindowsAPI;

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
            
        }

        [When(@"User clicks button with proper number to access app")]
        public void ClickButtonWithProperNumberToAccessApp()
        {
            StartView.ButtonToStartUsingApp(StartView.PanelWelcome.GetText()).Click();
        }

        [Then(@"Main window is open")]
        public void ThenMainWindowIsOpen()
        {
            
        }

        [When(@"User opens folder ""(.*)"" in ""(.*)"" panel")]
        public void WhenUserOpensFolderInPanel(string folder, string panel)
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindow(ConfigurationManager.AppSettings["MainWindowName"]);
            MainView.ListBoxPanel(GetPanelIndex(panel)).Click();
            Thread.Sleep(2000);
            OpenPath(folder);
            Thread.Sleep(2000);
        }

        [Then(@"folder ""(.*)"" is open in ""(.*)"" panel")]
        public void ThenFolderIsOpenInPanel(string p0, string p1)
        {
            
        }

        public void OpenPath(string folder)
        {
            Thread.Sleep(2000);
            var textline = Scope.DefaultWindow.Get(SearchCriteria.ByControlType(ControlType.Pane).AndByText("c:\\*.*"));
            textline.Click();
            Thread.Sleep(2000);
            var textBox = textline.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.SetValue(folder);
            textBox.KeyIn(KeyboardInput.SpecialKeys.RETURN);
        }

        [When(@"User moves ""(.*)"" from ""(.*)"" panel to ""(.*)""")]
        public void WhenUserMovesFromPanelTo(string filename, string fromPanel, string toPanel)
        {
            MoveFile(GetPanelIndex(fromPanel), filename, GetPanelIndex(toPanel));
        }

        [Then(@"Confirmation window is open")]
        public void ThenConfirmationWindowIsOpen()
        {
            
        }

        [When(@"User confirms file movement")]
        public void WhenUserConfirmsFileMovement()
        {
            MoveConfirmationView.ButtonConfirmMovement.Click();
            Thread.Sleep(2000);
            /*SelectItemInContextMenu(1, "file1", "Cut");
            Thread.Sleep(2000);
            SelectItemInContextMenu(0, "Paste");*/
        }

        [Then(@"File ""(.*)"" is moved to folder ""(.*)"" on ""(.*)"" panel")]
        public void ThenFileIsMovedToFolderOnPanel(string p0, string p1, string p2)
        {
            
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
    }
}

using System.Windows;
using System.Configuration;
using System.Threading;
using TechTalk.SpecFlow;
using TestStack.White.InputDevices;
using TestStackFramework.framework;
using TestStackFramework.utils;
using Views;

namespace Tests.steps
{
    [Binding]
    public class TotalCommanderTestSteps
    {
        [Given(@"Application was launched, folders ""(.*)"", ""(.*)"" with files ""(.*)"", ""(.*)""  were created if was needed")]
        public void CreateFoldersWithFilesAndStartApp(string folder1, string folder2, string filename1, string filename2)
        {
            FileUtil fileUtil = new FileUtil();
            fileUtil.CreateFolder(folder1);
            fileUtil.CreateFolder(folder2);
            fileUtil.CreateFile(folder1, filename1);
            fileUtil.CreateFile(folder2, filename2);
            Scope.Application = MyApp.Launch(ConfigurationManager.AppSettings["Path"], ConfigurationManager.AppSettings["EXE"]);
            Scope.DefaultWindow = MyApp.Window;
        }

        [Given(@"User selects button with proper number to access app")]
        public void ClickButtonWithProperNumberToAccessApp()
        {
            StartView.ButtonToStartUsingApp(StartView.PanelWelcome.GetText()).Click();
            Thread.Sleep(2000);
        }

        [When(@"User opens folder ""(.*)"" in left panel, folder ""(.*)"" in right panel and moves ""(.*)"" from left panel to right")]
        public void WhenUserOpensFolderInLeftPanelFolderInRightPanelAndMovesFromLeftPanelToRight(string firstFolder, string secondFolder, string filename)
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindow(ConfigurationManager.AppSettings["MainWindowName"]);
            OpenFolder(0, firstFolder);
            OpenFolder(1, secondFolder);
            MoveFile(0, filename, 1);
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
            foreach (var listBoxItem in MainView.ListBoxPanel(fromPanel).GetItems())
            {
                if (!listBoxItem.Name.Contains(filename)) continue;
                LoggerUtil.Info($"List Item {filename} is found, getting clickable point");
                fileCoordinates = listBoxItem.ClickablePoint;
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
    }
}

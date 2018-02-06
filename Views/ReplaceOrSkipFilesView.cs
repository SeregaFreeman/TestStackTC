using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using TestStackFramework.framework.elements;

namespace Views
{
    public class ReplaceOrSkipFilesView
    {
        public static Button ButtonSkip => Button.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("Skip this file"), "Skip");
        public static Button ButtonReplace => Button.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("Replace the file in the destination"), "Replace");
    }
}
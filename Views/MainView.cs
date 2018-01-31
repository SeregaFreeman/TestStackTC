using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using TestStackFramework.framework.elements;

namespace Views
{
    public class MainView
    {
        public static ListBox ListBoxSidePanel => ListBox.Get(SearchCriteria.ByText("W_TreeList1"), "Side panel");
        public static ListBox ListBoxPanel(int index) =>
            ListBox.Get(SearchCriteria.ByControlType(ControlType.List).AndByClassName("LCLListBox").AndIndex(index), "Panel");
        public static Button ButtonCloseEditCommentDialog => Button.Get(SearchCriteria.ByText("OK"), "Close 'edit comment' dialog");
    }
}

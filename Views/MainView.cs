using System.Collections.Generic;
using System.Windows.Automation;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStackFramework.framework.elements;
using Button = TestStackFramework.framework.elements.Button;
using Label = TestStackFramework.framework.elements.Label;
using Panel = TestStackFramework.framework.elements.Panel;
using TextBox = TestStackFramework.framework.elements.TextBox;

namespace Views
{
    public class MainView
    {
        public static ListBox ListBoxSidePanel => ListBox.Get(SearchCriteria.ByText("W_TreeList1"), "Side panel");

        public static ListBox ListBoxPanel(int index) =>
            ListBox.Get(SearchCriteria.ByControlType(ControlType.List).AndByClassName("LCLListBox").AndIndex(index), "File Panel");

        public static Label LabelNoFilesSelected(Window window) =>
            Label.Get(SearchCriteria.ByText("No files selected!"), "No files selected", window);

        public static Button ButtonCloseEditCommentDialog => Button.Get(SearchCriteria.ByText("OK"), "Close 'edit comment' dialog");
        public static Button ButtonStartSearch => Button.Get(SearchCriteria.ByText("Start search"), "Start search");

        public static List<Panel> Panels => Panel.GetMultiple(SearchCriteria.ByControlType(ControlType.Pane));

        public static TextBox PathTextBox(UIItem item) => TextBox.Get(SearchCriteria.ByControlType(ControlType.Edit), "Edit path", item);

        public static Panel PanelCurrentDirectory(string directory) => Panel.Get(SearchCriteria.ByText(directory), "Current panel directory");

        public static MenuBar MenuBarApplication => MenuBar.Get(SearchCriteria.ByText("Application"), "Main app menu");
    }
}
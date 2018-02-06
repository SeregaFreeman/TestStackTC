using System.Collections.Generic;
using System.Windows.Automation;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStackFramework.framework.elements;
using Button = TestStackFramework.framework.elements.Button;
using Panel = TestStackFramework.framework.elements.Panel;
using TextBox = TestStackFramework.framework.elements.TextBox;
using CheckBox = TestStackFramework.framework.elements.CheckBox;

namespace Views
{
    public class MainView
    {
        public static ListBox ListBoxSidePanel => ListBox.Get(SearchCriteria.ByText("W_TreeList1"), "Side panel");
        public static ListBox ListBoxPanel(int index) =>
            ListBox.Get(SearchCriteria.ByControlType(ControlType.List).AndByClassName("LCLListBox").AndIndex(index), "Panel");
        public static ListBox ListBoxSearchResults => ListBox.Get(SearchCriteria.ByClassName("LCLListBox"), "Search results");

        public static ListItem ListItemSearchResultsCount(int files, int directories) =>
            ListItem.Get(SearchCriteria.ByText($" [{files} files and {directories} directories found]"), "Results count");
        public static ListItem ListItemSearchResult(string name) =>
            ListItem.Get(SearchCriteria.ByText(name), "Result");

        public static Button ButtonCloseEditCommentDialog => Button.Get(SearchCriteria.ByText("OK"), "Close 'edit comment' dialog");
        public static Button ButtonStartsearch => Button.Get(SearchCriteria.ByText("Start search"), "Start search");

        public static List<Panel> Panels => Panel.GetMultiple(SearchCriteria.ByControlType(ControlType.Pane));

        public static TextBox PathTextBox(UIItem item) => TextBox.Get(SearchCriteria.ByControlType(ControlType.Edit), "Edit path", item);
        public static TextBox TextBoxSearchFor => TextBox.Get(SearchCriteria.Indexed(0), "Search for");
        public static TextBox TextBoxSearchIn => TextBox.Get(SearchCriteria.Indexed(1), "Search in");

        public static MenuBar MenuBarApplication => MenuBar.Get(SearchCriteria.ByText("Application"), "Main app menu");

        public static Tab TabFindFiles => Tab.Get(SearchCriteria.ByClassName("SysTabControl32"), "Find files");
        
        public static CheckBox CheckBoxRegEx => CheckBox.Get(SearchCriteria.ByText("RegEx"), "RegEx");
    }
}

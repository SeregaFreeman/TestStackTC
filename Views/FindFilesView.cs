using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStackFramework.framework;
using TestStackFramework.framework.elements;

namespace Views
{
    public class FindFilesView
    {
        public static readonly Window FindFilesWindow = Scope.DefaultWindow.ModalWindow("Find Files");

        public static ListBox ListBoxSearchResults =>
            ListBox.Get(SearchCriteria.ByClassName("LCLListBox"), "Search results", FindFilesWindow);

        public static ListItem ListItemSearchResultsCount(int files, int directories) =>
            ListItem.Get(SearchCriteria.ByText($" [{files} files and {directories} directories found]"), "Results count", FindFilesWindow);

        public static ListItem ListItemSearchResult(string name) =>
            ListItem.Get(SearchCriteria.ByText(name), "Result", FindFilesWindow);

        public static Button ButtonCloseWindow => Button.Get(SearchCriteria.ByAutomationId("Close"), "Close Window", FindFilesWindow);

        public static TextBox TextBoxSearchFor => TextBox.Get(SearchCriteria.Indexed(0), "Search for", FindFilesWindow);
        public static TextBox TextBoxSearchIn => TextBox.Get(SearchCriteria.Indexed(1), "Search in", FindFilesWindow);

        public static Tab TabFindFiles => Tab.Get(SearchCriteria.ByClassName("SysTabControl32"), "Find files", FindFilesWindow);

        public static CheckBox CheckBoxRegEx => CheckBox.Get(SearchCriteria.ByText("RegEx"), "RegEx", FindFilesWindow);
    }
}
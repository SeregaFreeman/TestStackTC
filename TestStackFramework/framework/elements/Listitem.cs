using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework.elements
{
    public class ListItem : BaseUiItem<TestStack.White.UIItems.ListBoxItems.ListItem>
    {
        protected ListItem(TestStack.White.UIItems.ListBoxItems.ListItem uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static ListItem Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new ListItem(Find(searchCriteria, window), itemName);
        }
    }
}

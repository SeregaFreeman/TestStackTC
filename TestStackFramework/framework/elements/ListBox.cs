using System.Windows;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework.elements
{
    public class ListBox : BaseUiItem<TestStack.White.UIItems.ListBoxItems.ListBox>
    {
        protected ListBox(TestStack.White.UIItems.ListBoxItems.ListBox uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static ListBox Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new ListBox(Find(searchCriteria, window), itemName);
        }

        public ListItems GetItems()
        {
            return _uiItem.Items;
        }

        public Rect GetBounds()
        {
            return _uiItem.Bounds;
        }
    }
}

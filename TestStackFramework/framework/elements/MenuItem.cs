using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework.elements
{
    public class MenuItem : BaseUiItem<Menu>
    {
        protected MenuItem(Menu uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static MenuItem Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new MenuItem(Find(searchCriteria, window), itemName);
        }
    }
}

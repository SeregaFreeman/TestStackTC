using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStackFramework.utils;

namespace TestStackFramework.framework.elements
{
    public class Tab : BaseUiItem<TestStack.White.UIItems.TabItems.Tab>
    {
        protected Tab(TestStack.White.UIItems.TabItems.Tab uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static Tab Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new Tab(Find(searchCriteria, window), itemName);
        }

        public void SelectTabItem(string itemName)
        {
            LoggerUtil.Info($"Selecting {itemName} from {_uiItem.Name} tab");
            _uiItem.SelectTabPage(itemName);
        }

        public string GetSelectedTabName()
        {
            LoggerUtil.Info($"Getting selected tab name from {_uiItem.Name} tab");
            return _uiItem.SelectedTab.Name;
        }
    }
}

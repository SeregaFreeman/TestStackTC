using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.WindowItems;
using TestStackFramework.utils;

namespace TestStackFramework.framework.elements
{
    public class Tab : BaseUiItem<TestStack.White.UIItems.TabItems.Tab>
    {
        public ITabPage SelectedTab
        {
            get
            {
                LoggerUtil.Info($"Getting selected tab name from {UiItem.Name} tab");
                return UiItem.SelectedTab;
            }
        }

        protected Tab(TestStack.White.UIItems.TabItems.Tab uiItem, string itemName) : base(uiItem, itemName)
        {
        }

        public static Tab Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new Tab(Find(searchCriteria, window), $"Tab: {itemName}");
        }

        public void SelectTabItem(string itemName)
        {
            LoggerUtil.Info($"Selecting {itemName} from {UiItem.Name} tab");
            UiItem.SelectTabPage(itemName);
        }
    }
}
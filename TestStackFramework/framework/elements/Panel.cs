using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework.elements
{
    public class Panel : BaseUiItem<TestStack.White.UIItems.Panel>
    {
        protected Panel(TestStack.White.UIItems.Panel uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static Panel Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new Panel(Find(searchCriteria, window), itemName);
        }

        public string GetText()
        {
            return _uiItem.Text;
        }

    }
}

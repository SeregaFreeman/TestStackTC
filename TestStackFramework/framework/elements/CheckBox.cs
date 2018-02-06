using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework.elements
{
    public class CheckBox : BaseUiItem<TestStack.White.UIItems.CheckBox>
    {
        protected CheckBox(TestStack.White.UIItems.CheckBox uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static CheckBox Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new CheckBox(Find(searchCriteria, window), itemName);
        }
    }
}

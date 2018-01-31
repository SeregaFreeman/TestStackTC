using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework.elements
{
    public class Button : BaseUiItem<TestStack.White.UIItems.Button>
    {

        protected Button(TestStack.White.UIItems.Button uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static Button Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new Button(Find(searchCriteria, window), itemName);
        }
    }
}

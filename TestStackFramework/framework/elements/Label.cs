using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework.elements
{
    public class Label : BaseUiItem<TestStack.White.UIItems.Label>
    {
        protected Label(TestStack.White.UIItems.Label uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static Label Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new Label(Find(searchCriteria, window), itemName);
        }
    }
}

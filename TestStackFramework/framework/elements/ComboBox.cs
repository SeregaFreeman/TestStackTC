using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework.elements
{
    public class ComboBox : BaseUiItem<TestStack.White.UIItems.ListBoxItems.ComboBox>
    {
        protected ComboBox(TestStack.White.UIItems.ListBoxItems.ComboBox uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static ComboBox Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new ComboBox(Find(searchCriteria, window), itemName);
        }

    }
}

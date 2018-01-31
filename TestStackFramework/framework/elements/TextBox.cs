using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStackFramework.utils;

namespace TestStackFramework.framework.elements
{
    public class TextBox : BaseUiItem<TestStack.White.UIItems.TextBox>
    {
        protected TextBox(TestStack.White.UIItems.TextBox uiItem, string itemName) : base(uiItem, itemName)
        {

        }

        public static TextBox Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new TextBox(Find(searchCriteria, window), itemName);
        }

        public void BulkText(string text)
        {
            LoggerUtil.Info($"Entering text into TextBox — {ItemName}. Value for enter — {text}");
            _uiItem.BulkText = text;
        }
    }
}

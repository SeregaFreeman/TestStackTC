using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;

namespace TestStackFramework.framework.elements
{
    public class ToolBar : BaseUiItem<ToolStrip>
    {
        protected ToolBar(ToolStrip uiItem, string itemName) : base(uiItem, itemName)
        {
        }

        public static ToolBar Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new ToolBar(Find(searchCriteria, window), $"ToolBar: {itemName}");
        }
    }
}
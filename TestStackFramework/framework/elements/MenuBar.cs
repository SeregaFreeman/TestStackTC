using System;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStackFramework.utils;

namespace TestStackFramework.framework.elements
{
    public class MenuBar : BaseUiItem<TestStack.White.UIItems.WindowStripControls.MenuBar>
    {
        protected MenuBar(TestStack.White.UIItems.WindowStripControls.MenuBar uiItem, string itemName) : base(uiItem, itemName)
        {
        }

        public static MenuBar Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new MenuBar(Find(searchCriteria, window), $"MenuBar: {itemName}");
        }

        public void SelectMenu(params string[] path)
        {
            string pathForLog = "";
            foreach (var pathitem in path)
            {
                pathForLog = String.Concat(pathForLog, $"{pathitem} => ");
            }
            LoggerUtil.Info($"Selecting {pathForLog} in {ItemName}");
            UiItem.MenuItem(path).Click();
        }
    }
}
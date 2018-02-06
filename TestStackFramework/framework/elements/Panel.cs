using System;
using System.Collections.Generic;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStackFramework.utils;

namespace TestStackFramework.framework.elements
{
    public class Panel : BaseUiItem<TestStack.White.UIItems.Panel>
    {
        protected Panel(TestStack.White.UIItems.Panel uiItem, string itemName) : base(uiItem, itemName)
        {
        }

        public static Panel Get(SearchCriteria searchCriteria, string itemName, Window window = null)
        {
            return new Panel(Find(searchCriteria, window), $"Panel: {itemName}");
        }

        public static List<Panel> GetMultiple(SearchCriteria searchCriteria, Window window = null)
        {
            var rawPanels = FindAll(searchCriteria, window);
            List<Panel> panels = new List<Panel>();
            foreach (var panel in rawPanels)
            {
                try
                {
                    panels.Add(new Panel((TestStack.White.UIItems.Panel)panel, "New panel..."));
                }
                catch (InvalidCastException ex)
                {
                    LoggerUtil.Error(ex.Message);
                }
            }
            return panels;
        }

        public string GetText()
        {
            return UiItem.Text;
        }
    }
}
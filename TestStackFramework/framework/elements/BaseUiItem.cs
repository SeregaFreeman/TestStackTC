using NUnit.Framework;
using System;
using System.Windows.Automation;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.WindowsAPI;
using TestStackFramework.utils;

namespace TestStackFramework.framework.elements
{
    public class BaseUiItem<T> where T : UIItem
    {
        protected T UiItem;

        public string Name => UiItem.Name;
        public string ItemName { get; }

        public UIItem RawItem => UiItem;

        public BaseUiItem(T uiItem, string name)
        {
            UiItem = uiItem;
            ItemName = name;
        }

        public static T Find(SearchCriteria searchCriteria, Window window = null)
        {
            if (window == null)
            {
                window = Scope.DefaultWindow;
            }

            T element = null;

            try
            {
                element = window.Get<T>(searchCriteria);
            }
            catch (AutomationException ex)
            {
                LoggerUtil.Info($"Element is not found: {ex}");
            }

            return element;
        }

        public static T Find(SearchCriteria searchCriteria, UIItem item)
        {
            T element = null;
            try
            {
                element = item.Get<T>(searchCriteria);
            }
            catch (AutomationException ex)
            {
                LoggerUtil.Info($"Element is not found: {ex}");
            }

            return element;
        }

        public static UIItem Find(TreeScope treeScope, AutomationProperty property, object value, Window window = null)
        {
            if (window == null)
            {
                window = Scope.DefaultWindow;
            }

            return new UIItem(window.AutomationElement.FindFirst(treeScope, new PropertyCondition(property, value)), new NullActionListener());
        }

        public static IUIItem[] FindAll(SearchCriteria searchCriteria, Window window = null)
        {
            if (window == null)
            {
                window = Scope.DefaultWindow;
            }

            try
            {
                return window.GetMultiple(searchCriteria);
            }
            catch (AutomationException ex)
            {
                LoggerUtil.Info($"Element is not found: {ex}");
            }
            throw new Exception("Element is not found");
        }

        public static UIItem FindItemByIndex(TreeScope treeScope, Condition condition, int index)
        {
            var elements = Scope.DefaultWindow.AutomationElement.FindAll(treeScope, condition);
            UIItem element = null;
            for (var i = 0; i < elements.Count; i++)
            {
                if (i == index)
                {
                    element = new UIItem(elements[i], new NullActionListener());
                }
            }
            Assert.NotNull(element);
            return element;
        }

        public void Click()
        {
            LoggerUtil.Info($"Performing click on {ItemName}");
            UiItem.Click();
        }

        public void RaiseClickEvent()
        {
            LoggerUtil.Info($"Performing raise click event on {ItemName}");
            UiItem.RaiseClickEvent();
        }

        public void SetValue(string value)
        {
            LoggerUtil.Info($"Setting value into — {ItemName}. Value for enter — {value}");
            UiItem.SetValue(value);
        }

        public void KeyIn(KeyboardInput.SpecialKeys key)
        {
            LoggerUtil.Info($"Sending key to — {ItemName}. Key for enter — {key}");
            UiItem.KeyIn(key);
        }

        public bool IsVisible()
        {
            try
            {
                return UiItem.Visible;
            }
            catch (Exception ex)
            {
                LoggerUtil.Info(ex.Message);
                return false;
            }
        }
    }
}
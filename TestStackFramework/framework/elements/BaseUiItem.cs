using System;
using System.Windows.Automation;
using TestStack.White;
using NUnit.Framework;
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
        private string _itemName;
        protected T _uiItem;

        public string Name => _uiItem.Name;
        public string ItemName => _itemName;
        public UIItem RawItem => _uiItem;

        public BaseUiItem(T uiItem, string name)
        {
            _uiItem = uiItem;
            _itemName = name;
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
                return element;
            }
            catch (AutomationException ex)
            {
                LoggerUtil.Info($"Element is not found: {ex}");
            }

            Assert.NotNull(element, "Element is not found");
            return element;
        }

        public static T Find(SearchCriteria searchCriteria, UIItem item)
        {
            T element = null;
            try
            {
                element = item.Get<T>(searchCriteria);
                return element;
            }
            catch (AutomationException ex)
            {
                LoggerUtil.Info($"Element is not found: {ex}");
            }

            Assert.NotNull(element, "Element is not found");
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
            _uiItem.Click();

        }

        public void RaiseClickEvent()
        {
            LoggerUtil.Info($"Performing raise click event on {ItemName}");
            _uiItem.RaiseClickEvent();
        }

        public void SetValue(string value)
        {
            LoggerUtil.Info($"Setting value into TextBox — {ItemName}. Value for enter — {value}");
            _uiItem.SetValue(value);
        }

        public void KeyIn(KeyboardInput.SpecialKeys key)
        {
            LoggerUtil.Info($"Sending key to — {ItemName}. Key for enter — {key}");
            _uiItem.KeyIn(key);
        }

        public bool IsVisible()
        {
            return _uiItem.Visible;
        }
    }
}

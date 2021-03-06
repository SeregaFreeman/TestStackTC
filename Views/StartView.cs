﻿using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using TestStackFramework.framework.elements;

namespace Views
{
    public class StartView
    {
        public static Panel PanelWelcome => Panel.Get(SearchCriteria.ByControlType(ControlType.Pane).AndIndex(3), "Welcome trial panel");

        public static Button ButtonToStartUsingApp(string text) =>
            Button.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText(text), "Button to start using app");
    }
}
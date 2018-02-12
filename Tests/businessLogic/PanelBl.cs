using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using TestStack.White.Utility;
using TestStack.White.WindowsAPI;
using TestStackFramework.framework;
using TestStackFramework.framework.elements;
using TestStackFramework.utils;
using Views;

namespace Tests.businessLogic
{
    public class PanelBl
    {
        public static void OpenPath(string panel, string folder)
        {
            var textlines = MainView.Panels;
            List<Panel> panels = new List<Panel>();
            panels.AddRange(textlines.Where(textline => textline.Name.Contains("*.*")));
            panels[GetPanelIndex(panel)].Click();
            MainView.PathTextBox(panels[GetPanelIndex(panel)].RawItem).SetValue(folder);
            MainView.PathTextBox(panels[GetPanelIndex(panel)].RawItem).KeyIn(KeyboardInput.SpecialKeys.RETURN);
        }

        public static void MoveFile(int fromPanel, string filename, int toPanel)
        {
            Point fileCoordinates = new Point();
            var item = MainView.ListBoxPanel(fromPanel).Items;
            foreach (var listBoxItem in item)
            {
                if (!listBoxItem.Name.Contains(filename)) continue;
                LoggerUtil.Info($"List Item {filename} is found, getting clickable point");
                listBoxItem.Click();
                fileCoordinates = new Point(listBoxItem.Bounds.BottomRight.X - 2,
                    listBoxItem.Bounds.BottomRight.Y - 2);
                break;
            }

            LoggerUtil.Info("Performing drag and drop");
            MouseInteractionUtil.DragAndDropByLocation(fileCoordinates, new Point(
                MainView.ListBoxPanel(toPanel).Bounds.BottomRight.X - 10,
                MainView.ListBoxPanel(toPanel).Bounds.BottomRight.Y - 10));
        }

        public static void SelectItemInContextMenu(int fromPanel, string itemName, string option)
        {
            foreach (var listBoxItem in MainView.ListBoxPanel(fromPanel).Items)
            {
                if (!listBoxItem.Name.Contains(itemName)) continue;
                LoggerUtil.Info($"List Item {itemName} is found");
                MouseInteractionUtil.SelectOptionInContextMenu(Scope.DefaultWindow,
                    new Point(listBoxItem.Bounds.X, listBoxItem.Bounds.Y), option);
                break;
            }
        }

        public static void SelectItemInContextMenu(int panel, string option)
        {
            MouseInteractionUtil.SelectOptionInContextMenu(Scope.DefaultWindow,
                new Point(MainView.ListBoxPanel(panel).Bounds.BottomLeft.X + 10,
                    MainView.ListBoxPanel(panel).Bounds.BottomLeft.Y - 10), option);
        }

        public static void SelectItemOnPanel(string itemName, string panel)
        {
            foreach (var panelItem in MainView.ListBoxPanel(GetPanelIndex(panel)).Items)
            {
                if (!panelItem.Name.Contains(itemName)) continue;
                LoggerUtil.Info($"Item {itemName} is found on {panel} panel");
                panelItem.Click();
            }
        }

        public static int GetPanelIndex(string panelName)
        {
            switch (panelName)
            {
                case "left":
                    return 0;

                case "right":
                    return 1;

                default:
                    throw new Exception("Incorrect panel name");
            }
        }

        public static bool IsFileFound(string panel, string file)
        {
            Retry.For(() => WindowBl.IsDefaultWindowOpen(ConfigurationManager.AppSettings["MainWindowName"]),
                TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"])));
            Scope.DefaultWindow.Focus();
            Retry.For(() => Scope.DefaultWindow.IsFocussed,
                TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"])));
            var loaded = Retry.For(() => MainView.ListBoxPanel(GetPanelIndex(panel)).Items.Exists(item => item.Name.Contains(file)),
                TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["defaultTimeout"])));
            LoggerUtil.Log.Info($"Finding file {file} on {panel} panel");
            return loaded;
        }
    }
}
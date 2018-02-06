using System.Threading;
using System.Windows;
using TestStack.White.InputDevices;

namespace TestStackFramework.utils
{
    public class MouseInteractionUtil
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MouseeventfRightdown = 0x08;
        public const int MouseeventfRightup = 0x10;

        public static void DragAndDropByLocation(Point initial, Point target)
        {
            Mouse.Instance.Location = initial;
            Mouse.LeftDown();
            Thread.Sleep(1000);
            Mouse.Instance.Location = target;
            Thread.Sleep(1000);
            Mouse.LeftUp();
        }

        public static void RightDown()
        {
            var x = (int)Mouse.Instance.Location.X;
            var y = (int)Mouse.Instance.Location.Y;
            mouse_event(MouseeventfRightdown, x, y, 0, 0);
        }

        public static void RightUp()
        {
            var x = (int)Mouse.Instance.Location.X;
            var y = (int)Mouse.Instance.Location.Y;
            mouse_event(MouseeventfRightup, x, y, 0, 0);
        }

        public static void RightClickWithDelay(int delay = 1000)
        {
            RightDown();
            Thread.Sleep(delay);
            RightUp();
        }
    }
}
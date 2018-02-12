using System.Linq;
using TestStackFramework.framework;

namespace Tests.businessLogic
{
    public class WindowBl
    {
        public static bool IsDefaultWindowOpen(string title)
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindows().FirstOrDefault();
            return Scope.DefaultWindow.Title == title;
        }

        public static bool IsDefaultWindowCurrentlyActive(string title)
        {
            Scope.DefaultWindow = Scope.Application.Application.GetWindows().FirstOrDefault();
            return Scope.DefaultWindow.IsCurrentlyActive;
        }
    }
}
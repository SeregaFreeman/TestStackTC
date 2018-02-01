using System.Configuration;
using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using TestStackFramework.framework;
using TestStackFramework.framework.elements;

namespace Views
{
    public class MoveConfirmationView
    {
        public static Button ButtonConfirmMovement = Button.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("OK"), "Confirm",
            Scope.DefaultWindow.ModalWindow(ConfigurationManager.AppSettings["ModalWindowName"]));
    }
}

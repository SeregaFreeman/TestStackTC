using System.IO;
using System.Linq;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace TestStackFramework.framework
{
    public class MyApp
    {
        public Application Application { get; set; }
        public static Window Window { get; set; }

        public MyApp(Application app, Window window)
        {
            Application = app;
            Window = window;
        }

        public static MyApp Launch(string path, string exe)
        {
            var app = Application.Launch(Path.Combine(path, exe));
            var window = app.GetWindows().FirstOrDefault();
            return new MyApp(app, window);
        }

        public void Kill()
        {
            Application.Kill();
        }
    }
}

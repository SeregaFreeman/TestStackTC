using System.Diagnostics;

namespace TestStackFramework.utils
{
    public class ProcessesUtil
    {
        public static void CloseAllProcessesByName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process p in processes)
            {
                p.Kill();
            }
        }
    }
}

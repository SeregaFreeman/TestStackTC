using SikuliSharp;
using System.IO;

namespace TestStackFramework.utils
{
    public class SikuliUtil
    {
        private static ISikuliSession session = Sikuli.CreateSession();

        public static void Click(string imagePath, string imageName, float accuracy)
        {
            session.Click(Patterns.FromFile(Path.Combine(imagePath, imageName), accuracy));
        }
    }
}
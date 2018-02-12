using System.IO;

namespace TestStackFramework.utils
{
    public class FileUtil
    {
        public static void CreateDirectory(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
        }

        public static void CreateFile(string path, string fileName)
        {
            var pathToFile = Path.Combine(path, fileName);
            if (!File.Exists(pathToFile))
            {
                File.Create(pathToFile);
            }
        }

        public static void DeleteFile(string path, string fileName)
        {
            var pathToFile = Path.Combine(path, fileName);
            if (File.Exists(pathToFile))
            {
                File.Delete(pathToFile);
            }
        }

        public static void DeleteDirectory(string path, bool recursive = false)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive);
            }
        }
    }
}
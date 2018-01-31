using System.IO;

namespace TestStackFramework.utils
{
    public class FileUtil
    {
        public void CreateFolder(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
        }

        public void CreateFile(string path, string fileName)
        {
            var pathToFile = Path.Combine(path, fileName);
            if (!File.Exists(pathToFile))
            {
                File.Create(pathToFile);
            }
        }
    }
}

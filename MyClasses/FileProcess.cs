using System.IO;

namespace MyClasses
{
    public class FileProcess
    {
        public bool FileExists(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                {
                throw new System.ArgumentNullException("fileName");
            }
            
            return File.Exists(fileName);

        }
    }
}

using System.IO;

namespace HexWorldUtils.General
{
    public class FileHandler
    {
        public bool FileExists(string fullPath) => File.Exists(fullPath);

        public bool TryReadFile(string fullPath, out string content)
        {
            if (FileExists(fullPath))
            {
                content = File.ReadAllText(fullPath);
                return true;
            }

            content = "";
            return false;
        }

        public void WriteFile(string path, string fileName, string content)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, fileName), content);
        }
    }
}

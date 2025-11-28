namespace HexWorldUtils.Resource.Models
{
    public class FileData<T>
    {
        public readonly T File;
        public readonly string FileName;

        public FileData(T file, string fileName)
        {
            File = file;
            FileName = fileName;
        }
    }
}

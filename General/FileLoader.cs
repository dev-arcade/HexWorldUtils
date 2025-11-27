namespace HexWorldUtils.General
{
    public class FileLoader
    {
        private readonly FileHandler _fileHandler;
        private readonly Serializer _serializer;

        public FileLoader()
        {
            _fileHandler = new FileHandler();
            _serializer = new Serializer();
        }

        public bool LoadFile<T>(string path, string fileName, out T data, out string error) where T : new()
        {
            if (_fileHandler.TryReadFile(path, out var content))
            {
                var successful = _serializer.DeserializeData(content, out data, out error);
                return successful && data != null;
            }

            content = _serializer.SerializeData(new T());
            _fileHandler.WriteFile(path, fileName, content);
            return _serializer.DeserializeData(content, out data, out error);
        }
    }
}

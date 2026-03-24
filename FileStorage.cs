using StoreSalesSystem.Application;
using StoreSalesSystem.Domain.Entities;
using StoreSalesSystem.Infrastructure;
using System.Text.Json;


namespace StoreSalesSystem
{
    public class FileStorage
    {
        private readonly string path;

        public FileStorage(string path)
        {
            this.path = path;
        }
        public FileStorage Load()
        {
            if (!File.Exists(path))
            {
                return new FileStorage();
            }

            var json = File.ReadAllText(path);

            var storage = JsonSerializer.Deserialize<FileStorage>(json);
            if (storage == null)
                throw new Exception("Deserialization return null.");

            return storage;

        }
        public void Save(FileStorage storage)
        {
            var json = JsonSerializer.Serialize(storage);
            File.WriteAllText(path, json);

        }
    }
}
using StoreSalesSystem.Application;

namespace StoreSalesSystem.Infrastructure
{
    internal class FileProductRepository : IProductRepository
    {
        private FileStorage storage;

        public FileProductRepository(FileStorage storage)
        {
            this.storage = storage;
        }
    }
}
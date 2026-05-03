using StoreSalesSystem.Application.Interfaces;

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
using StoreSalesSystem.Application;

namespace StoreSalesSystem.Infrastructure
{
    internal class FileCustomerRepository : ICustomerRepository
    {
        private FileStorage storage;

        public FileCustomerRepository(FileStorage storage)
        {
            this.storage = storage;
        }
    }
}
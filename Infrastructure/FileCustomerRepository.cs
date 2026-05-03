using StoreSalesSystem.Application.Interfaces;

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
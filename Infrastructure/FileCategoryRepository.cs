using StoreSalesSystem.Application;

namespace StoreSalesSystem.Infrastructure
{
    internal class FileCategoryRepository : ICategoryRepository
    {
        private FileStorage storage;

        public FileCategoryRepository(FileStorage storage)
        {
            this.storage = storage;
        }
    }
}
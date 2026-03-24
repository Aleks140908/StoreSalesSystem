using StoreSalesSystem.Application;

namespace StoreSalesSystem.Infrastructure
{
    internal class FilePromoRepository : IPromoCodeRepository
    {
        private FileStorage storage;

        public FilePromoRepository(FileStorage storage)
        {
            this.storage = storage;
        }
    }
}
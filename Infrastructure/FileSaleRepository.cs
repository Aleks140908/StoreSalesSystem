using StoreSalesSystem.Application;

namespace StoreSalesSystem.Infrastructure
{
   public class FileSaleRepository : ISaleRepository
    {
        private FileStorage storage;

        public FileSaleRepository(FileStorage storage)
        {
            this.storage = storage;
        }
    }
}
using StoreSalesSystem.Application;

namespace StoreSalesSystem.ConsoleUI
{
    public class SaleUI
    {
        private SaleService service;

        public SaleUI(SaleService service)
        {
            this.service = service;
        }

        internal void Run()
        {
            throw new NotImplementedException();
        }
    }
}
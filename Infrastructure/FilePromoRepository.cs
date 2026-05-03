using StoreSalesSystem.Application.Interfaces;
using StoreSalesSystem.Domain.Entities;

namespace StoreSalesSystem.Infrastructure
{
    internal class FilePromoRepository : IPromoCodeRepository
    {
        private FileStorage storage;

        public FilePromoRepository(FileStorage storage)
        {
            this.storage = storage;
        }

        public PromoCode Add(PromoCode code)
        {
            if (storage.PromoCodes.Any()) code.Id = storage.PromoCodes.Max(p => p.Id) + 1;
            else code.Id = 1;

            storage.PromoCodes.Add(code);
            storage.Save();
            return code;
        }
        public void Delete(int id)
        {
            var promo = GetById(id);
            if (promo == null) return;
            storage.PromoCodes.Remove(promo);
            storage.Save();
        }

        public IEnumerable<PromoCode> GetAll()
        {
            return storage.PromoCodes;
        }

        public PromoCode? GetByCode(string code)
        {
            return storage.PromoCodes.FirstOrDefault(p => p.Code == code);
        }

        public PromoCode? GetById(int id)
        {
            return storage.PromoCodes.FirstOrDefault(p => p.Id == id);
        }
        public void Update(PromoCode code)
        {
            var curCode = GetById(code.Id);
            if (curCode == null) return;

            curCode.Code = code.Code;
            curCode.Type = code.Type;
            curCode.Value = code.Value;
            curCode.ValidFrom = code.ValidFrom;
            curCode.ValidUntil = code.ValidUntil;
            curCode.IsActive = code.IsActive;

            storage.Save();
        }
    }
}
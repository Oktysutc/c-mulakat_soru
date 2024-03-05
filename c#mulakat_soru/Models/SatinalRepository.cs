using System.Linq.Expressions;
using c_mulakat_soru.Utility;

namespace c_mulakat_soru.Models
{
    public class SatinalRepository : Repository<Satinal>, ISatinalRepository
    {
        private  UygulamaDbContext _uygulamaDbContext;
        public SatinalRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Satinal Satinal)
        {
            _uygulamaDbContext.Update(Satinal);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}

using System.Linq.Expressions;
using c_mulakat_soru.Utility;

namespace c_mulakat_soru.Models
{
    public class KursRepository : Repository<Kurs>, IKursRepository
    {
        private  UygulamaDbContext _uygulamaDbContext;
        public KursRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Kurs kurs)
        {
            _uygulamaDbContext.Update(kurs);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}

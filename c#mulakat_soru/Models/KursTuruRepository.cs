using c_mulakat_soru.Utility;

namespace c_mulakat_soru.Models
{
    public class KursTuruRepository : Repository<KursTuru>, IKursTuruRepository
    {
        private  UygulamaDbContext _uygulamaDbContext;
        public KursTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(KursTuru kursTuru)
        {
            _uygulamaDbContext.Update(kursTuru);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}

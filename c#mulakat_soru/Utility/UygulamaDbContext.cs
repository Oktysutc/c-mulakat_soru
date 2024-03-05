using c_mulakat_soru.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
// veri tabanında ef core ile tablo olusturması için ilgili model sınıflarınızı buraya eklemelisiniz...
namespace c_mulakat_soru.Utility
{
    public class UygulamaDbContext : IdentityDbContext
    {//  uygulamanın  entity ile model arasındaki bağlantıları burada yapıldı
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<KursTuru> KursTurleri { get; set; }
        public DbSet<Kurs>Kurslar { get; set; }
        public DbSet<Satinal> Satinalmalar { get; set; }

    }
}

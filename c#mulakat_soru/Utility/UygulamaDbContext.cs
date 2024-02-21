using c_mulakat_soru.Models;
using Microsoft.EntityFrameworkCore;

namespace c_mulakat_soru.Utility
{
    public class UygulamaDbContext : DbContext
    {//  uygulamanın  entity ile model arasındaki bağlantıları burada yapıldı
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<KursTuru> KursTurleri { get; set; }
        public DbSet<Kurs>Kurslar { get; set; }
    }
}

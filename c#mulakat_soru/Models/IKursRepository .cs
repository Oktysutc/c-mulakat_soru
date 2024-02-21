namespace c_mulakat_soru.Models
{
    public interface IKursRepository : IRepository<Kurs>
    {
        void Guncelle(Kurs kurs);
        void Kaydet();

    }
}

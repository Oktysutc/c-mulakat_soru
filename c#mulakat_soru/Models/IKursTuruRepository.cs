namespace c_mulakat_soru.Models
{
    public interface IKursTuruRepository : IRepository<KursTuru>
    {
        void Guncelle(KursTuru kursTuru);
        void Kaydet();

    }
}

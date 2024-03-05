namespace c_mulakat_soru.Models
{
    public interface ISatinalRepository : IRepository<Satinal>
    {
        void Guncelle(Satinal Satinal);
        void Kaydet();

    }
}

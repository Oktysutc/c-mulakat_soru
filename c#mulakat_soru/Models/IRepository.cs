using System.Linq.Expressions;

namespace c_mulakat_soru.Models
{
    public interface IRepository<T> where T : class
    {
        //T->kitapturu
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filtre);
        void Ekle(T entity);
        void Sil(T entity);
        void SilAralık(IEnumerable<T> entities);
    }
}

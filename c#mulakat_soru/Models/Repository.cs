﻿using c_mulakat_soru.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.Versioning;

namespace c_mulakat_soru.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        internal DbSet<T> dbSet;
        public Repository(UygulamaDbContext uygulamaDbContext)
        {
            _uygulamaDbContext= uygulamaDbContext;
            this.dbSet = _uygulamaDbContext.Set<T>();
            _uygulamaDbContext.Kurslar.Include(k => k.kursTuru).Include(k => k.KursTuruId);
        }
        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu = sorgu.Where(filtre);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps=null)
        {
            IQueryable<T> sorgu = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach(var includeProp in includeProps.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu=sorgu.Include(includeProp);
                }
            }
            return sorgu.ToList();

        }

        public void Sil(T entity)
        {
            dbSet.Remove(entity);
        }
        public void SilAralık(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}

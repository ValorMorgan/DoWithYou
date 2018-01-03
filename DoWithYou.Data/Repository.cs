using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Entities.DoWithYou.Base;
using DoWithYou.Interface;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Data
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        #region VARIABLES
        private readonly DoWithYouContext _context;
        private readonly DbSet<T> _entities;
        #endregion

        #region CONSTRUCTORS
        public Repository(DoWithYouContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        #endregion

        public void Delete(T entity)
        {
            if (entity == default(T))
                return;

            _entities.Remove(entity);
            SaveChanges();
        }

        public T Get(long id) => _entities.SingleOrDefault(e => e.ID == id);

        public IEnumerable<T> GetAll() => _entities.AsEnumerable();

        public void Insert(T entity)
        {
            if (entity == default(T))
                return;

            _entities.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == default(T))
                return;

            _entities.Update(entity);
            SaveChanges();
        }
    }
}
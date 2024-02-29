﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace SocialNetWork.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _db;

        public DbSet<T> Set
        {
            get;
            private set;
        }

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            var set = _db.Set<T>();
            set.Load();

            Set = set;
        }

        public void Create(T item)
        {
            Set.Add(item);
            _db.SaveChanges();
        }

        public void Delete(T item)
        {
            Set.Remove(item);
            _db.SaveChanges();
        }

        public T Get(int id)
        {
            return Set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Set;
        }

        public void Update(T item)
        {
            Set.Update(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

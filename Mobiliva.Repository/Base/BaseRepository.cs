using System;
using Mobiliva.Core.BaseRepository;
using Mobiliva.Core.Entity;
using Mobiliva.DAL;
using static Mobiliva.Core.Enums.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Mobiliva.Repository.Base
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected ApplicationContext _context;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            if (entity.CreateDate == null)
                entity.CreateDate = DateTime.UtcNow;
            _context.Set<T>().Add(entity);
            Save();
        }

        public virtual void Update(T entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            _context.Entry<T>(entity).CurrentValues.SetValues(entity);
            Save();
        }

        public virtual void Delete(T entity)
        {
            entity.RecordStatus = RecordStatus.Deleted;
            entity.UpdateDate = DateTime.UtcNow;
            _context.Entry<T>(entity).CurrentValues.SetValues(entity);
            //_context.Set<T>().Remove(entity);
            Save();
        }

        public virtual T GetById(long Id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == Id && x.RecordStatus != RecordStatus.Deleted);
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().Where(x => x.RecordStatus != RecordStatus.Deleted).ToList();
        }

        public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp).Where(x => x.RecordStatus != RecordStatus.Deleted);
        }

        public virtual T FirstOrDefaultBy(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(x => x.RecordStatus != RecordStatus.Deleted).FirstOrDefault(exp);
        }

        public virtual bool Any(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(x => x.RecordStatus != RecordStatus.Deleted).Any(exp);
        }

        public int Count()
        {
            return _context.Set<T>().Where(x => x.RecordStatus != RecordStatus.Deleted).Count();
        }

        public int Count(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(x => x.RecordStatus != RecordStatus.Deleted).Count(exp);
        }

        public T GetById(long Id, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(x => x.Id == Id && x.RecordStatus != RecordStatus.Deleted);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).FirstOrDefault();

        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(exp).Where(x => x.RecordStatus != RecordStatus.Deleted);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public T FirstOrDefaultBy(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(x => x.RecordStatus != RecordStatus.Deleted).Where(exp);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).FirstOrDefault();
        }

        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            Save();
        }

        public void UpdateRange(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            Save();
        }

        public void DeleteRange(List<T> entities)
        {
            entities.ForEach(x =>
            {
                x.RecordStatus = RecordStatus.Deleted;
            });
            _context.Set<T>().UpdateRange(entities);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}


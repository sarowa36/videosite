using EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public abstract class GenericBaseRepository<T> where T : AOfDefaultContent
    {
        protected readonly ADC db;
        public GenericBaseRepository(ADC db)
        {
            this.db = db;
        }
        public virtual void Create(T t)
        {
            db.Set<T>().Add(t);
            db.SaveChanges();
        }
        public virtual T Get(int id)
        {
            return db.Set<T>().Find(id);
        }
        public virtual T Get(int id,IQueryable<T> filter)
        {
            return filter.FirstOrDefault(x=>x.Id==id);
        }
        public virtual List<T> GetAll(IQueryable<T> list, int? index = null, int? count = null)
        {
            if (index != null)
                list = list.Skip((int)index);
            if (count != null)
                list = list.Take((int)count);
            return list.ToList();
        }

        public virtual List<T> GetAll(int? index = null, int? count = null)
        {
            return GetAll(db.Set<T>().AsQueryable(), index, count);
        }
        public virtual void Update(T t)
        {
            db.Set<T>().Update(t);
            db.SaveChanges();
        }
        public virtual void Delete(T t)
        {
            t.IsDeleted = true;
            db.Set<T>().Update(t);
            db.SaveChanges();
        }
        public virtual void Delete(int id)
        {
            var val = db.Set<T>().Find(id);
            if (val != null)
                Delete(val);
        }
    }
}

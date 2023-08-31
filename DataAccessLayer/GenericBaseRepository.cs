using DataAccessLayer.ParamaterPass;
using EntityLayer.Base;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public abstract class GenericBaseRepository<T,TParam>:GenericBaseRepository<T> where T : AOfDefaultContent where TParam:IParamaterPass<T>
    {
        public GenericBaseRepository(ADC db):base(db)
        {
        }
        public virtual string? Create(T t,TParam param)
        {
            return Create(t);
        }
        public virtual T Get(int id,TParam param)
        {
            return Get(id);
        }

        public virtual List<T> GetAll(TParam param,int? index = null, int? count = null)
        {
            return GetAll(index, count);
        }
        public virtual string? Update(T t, TParam param)
        {
            return Update(t);
        }
        public virtual string? Delete(T t, TParam param)
        {
            return Delete(t);
        }
        public virtual string? Delete(int id, TParam param)
        {
            return Delete(id);
        }
    }
    public abstract class GenericBaseRepository<T> where T : AOfDefaultContent 
    {
        protected readonly ADC db;
        public GenericBaseRepository(ADC db)
        {
            this.db = db;
        }
        public virtual string? Create(T t)
        {
           db.Set<T>().Add(t);
            return SaveChanges();
        }
        public virtual T? Get(int id)
        {
            return db.Set<T>().Find(id);
        }
        public virtual bool Get(int id, out T? t)
        {
            t = this.Get(id);
            return t != null;
        }
        public virtual bool Exist(int id)
        {
            return db.Set<T>().Any(x=> x.Id == id);
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
        public virtual string? Update(T t)
        {
            db.Set<T>().Update(t);
           return SaveChanges();
        }
        public virtual string? Delete(T t)
        {
            t.IsDeleted = true;
            db.Set<T>().Update(t);
            return SaveChanges();
        }
        public virtual string? Delete(int id)
        {
            var val = db.Set<T>().Find(id);
            if (val != null)
               return Delete(val);
            return null;
        }
        protected virtual string? SaveChanges()
        {
            string? val = null;
            try { 
            db.SaveChanges();
            }
            catch(Exception ex) {
                val = ex.Message;
            }
            return val;
        }
    }
}

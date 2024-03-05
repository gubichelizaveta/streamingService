using course_project.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Repositories
{
    public class  repository<T> : Irepository<T> where T : class
    {
        protected readonly MyPlayerDbContext Context;

        #region Конструктор

        #endregion

        #region Методы интерфейса
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public void Add(T item)
        {
            Context.Set<T>().Add(item);
        }
        public void Remove(T item)
        {
            Context.Set<T>().Remove(item);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            Context.Set<T>().Attach(item);
            Context.Entry(item).State = EntityState.Modified;
        }
        public void Update(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }
        public T FindById(int id)
        {
            return Context.Set<T>().Find(id);
        }
        //public virtual IEnumerable<T> GetAllWithPropertiesIncluded()
        //    =>
        //     Context.Set<T>().Include(Context.GetIncludePaths<T>()).ToList();

        #endregion
    }
}

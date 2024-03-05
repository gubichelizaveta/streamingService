using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Repositories
{
    public interface Irepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        void Add(T item);

        void Update(T item);
        void Update(IEnumerable<T> item);

        T FindById(int id);

        void Remove(T item);
       // IEnumerable<T> GetAllWithPropertiesIncluded();
    }
}

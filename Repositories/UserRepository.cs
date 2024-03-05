using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using course_project.Model;

namespace course_project.Repositories
{
    public class UserRepository
    {
        /// protected readonly MyPlayerDbContext context = new MyPlayerDbContext();
        private MyPlayerDbContext db;

        ////////поиск по логину(существует ли пользователь)

        //////public bool IsThereUser(string login)
        //////{

        //////    var log = context.Users.Find(login);
        //////    if (log != null)
        //////        return false;//пользователь существует
        //////    else
        //////        return true;
        //////}
        /// private OrderContext db;

        public UserRepository(MyPlayerDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.AsNoTracking().ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
            db.SaveChanges();
        }

    }
}

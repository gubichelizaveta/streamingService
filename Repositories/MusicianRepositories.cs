using course_project.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Repositories
{
    public class MusicianRepositories
    {
        private MyPlayerDbContext context = new MyPlayerDbContext();
        //поиск по названию трека или имени артиста 

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

        public MusicianRepositories(MyPlayerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Musician> GetAll()
        {
            return context.Musicians.AsNoTracking().ToList();
        }

        public Musician Get(string artist_name)
        {
            return context.Musicians.Find(artist_name);
        }
        public Musician Get_Count(int artist_count)
        {
            return context.Musicians.Find(artist_count);
        }

        public Musician Find(object[] value)
        {
            return context.Musicians.Find(value);
        }
        public void Create(Musician artist)
        {
            context.Musicians.Add(artist);
            context.SaveChanges();
        }

        public void Update(Musician artist)
        {
            context.Entry(artist).State = EntityState.Modified;
        }

        public void Delete(string name)
        {
            Musician artist = context.Musicians.Find(name);
            if (artist != null)
                context.Musicians.Remove(artist);
            context.SaveChanges();
        }

    }
}

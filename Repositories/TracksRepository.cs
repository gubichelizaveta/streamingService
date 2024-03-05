using course_project.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Repositories
{
    public class TracksRepository
    {
        private  MyPlayerDbContext context = new MyPlayerDbContext();
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

        public TracksRepository(MyPlayerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Tracks> GetAll()
        {
            return context.Tracks.AsNoTracking().ToList();
        }

        public Tracks Get(string  track_name)
        {
            return context.Tracks.Find(track_name);
        }
        public Tracks Get_Count(int track_count)
        {
            return context.Tracks.Find(track_count);
        }

        public Tracks Find(object[] value)
        {
            return context.Tracks.Find(value);
        }
        public void Create(Tracks track)
        {
            context.Tracks.Add(track);
            context.SaveChanges();
        }

        public void Update(Tracks track)
        {
            context.Entry(track).State = EntityState.Modified;
        }

        public void Delete(string name)
        {
            Tracks track = context.Tracks.Find(name);
            if (track != null)
                context.Tracks.Remove(track);
            context.SaveChanges();
        }

    }
}

using course_project.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Repositories
{
    public class PlaylistRepository
    {
        private MyPlayerDbContext context = new MyPlayerDbContext();

        public PlaylistRepository(MyPlayerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Playlists> GetAll()
        {
            return context.Playlists.AsNoTracking().ToList();
        }

        public Playlists Get(int id)
        {
            return context.Playlists.Find(id);
        }
        public void Create(Playlists playlist)
        {
            context.Playlists.Add(playlist);
            context.SaveChanges();
        }

        public void Update(Playlists playlist)
        {
            context.Entry(playlist).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Playlists playlist = context.Playlists.Find(id);
            if (playlist != null)
                context.Playlists.Remove(playlist);
            context.SaveChanges();
        }

    }
}

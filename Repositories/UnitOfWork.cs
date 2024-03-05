using course_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Repositories
{
    public class UnitOfWork :IDisposable
    {
        private MyPlayerDbContext db = new MyPlayerDbContext();
        private FiltrRepository FiltrRepository;
        private LikedRepository LikedRepository;
        private MusicianRepositories MusicianRepositories;
        private PlaylistRepository PlaylistRepository;
        private TracksRepository TracksRepository;
        private UserRepository UserRepository;

        public UserRepository User
        {
            get
            {
                if (UserRepository == null)
                    UserRepository = new UserRepository(db);
                return UserRepository;
            }
        }
        public  FiltrRepository Filtrs
        {
            get
            {
                if (FiltrRepository == null)
                    FiltrRepository = new FiltrRepository();
                return FiltrRepository;
            }
        }
        public TracksRepository Tracks
        {
            get
            {
                if (TracksRepository == null)
                    TracksRepository = new TracksRepository(db);
                return TracksRepository;
            }
        }
        public PlaylistRepository Playlist
        {
            get
            {
                if (PlaylistRepository == null)
                    PlaylistRepository = new PlaylistRepository(db);
                return PlaylistRepository;
            }
        }
        public MusicianRepositories Musician
        {
            get
            {
                if (MusicianRepositories == null)
                    MusicianRepositories = new MusicianRepositories(db);
                return MusicianRepositories;
            }
        }
        public LikedRepository Likes
        {
            get
            {
                if (LikedRepository == null)
                    LikedRepository = new LikedRepository();
                return LikedRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

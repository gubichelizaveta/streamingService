using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Model
{
    public class MyPlayerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tracks> Tracks { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<LikedTracks> LikedTracks { get; set; }
        public DbSet<Playlists> Playlists { get; set; }
        public DbSet<ForFiltr> Filtrs { get; set; }

        public MyPlayerDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-5SFL9AJ; Database = AudioPlayer; Trusted_Connection = True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.user_filtr)
                .WithOne(l => l.User_Id);
            modelBuilder.Entity<User>()
                .HasMany(k => k.user_like)
                .WithOne(a => a.User_Id);
            modelBuilder.Entity<User>()
                .HasMany(q => q.user_playlists)
                .WithOne(w => w.User_Id);
            modelBuilder.Entity<Tracks>()
                .HasMany(r => r.likedTracks)
                .WithOne(t => t.Track);
            modelBuilder.Entity<Tracks>()
                .HasMany(f => f.tracksinplaylist)
                .WithOne(h => h.Track);
            modelBuilder.Entity<Musician>()
                .HasMany(d => d.favoriteartist)
                .WithOne(k => k.Liked_Artist);
            //modelBuilder.Entity<Musician>()
            //    .HasMany(v => v.tracks)
            //    .WithOne(m => m.Artist);


            modelBuilder.Entity<ForFiltr>()
                .HasKey(f => f.Filtr_ID);
            modelBuilder.Entity<LikedTracks>()
                .HasKey(l => l.List_ID);
            modelBuilder.Entity<Musician>()
                .HasKey(an => an.Artist_Name);
            modelBuilder.Entity<Playlists>()
                .HasKey(pi => pi.Playlist_ID);
            modelBuilder.Entity<Tracks>()
                .HasKey(t => t.Track_Name);
            modelBuilder.Entity<User>()
                .HasKey(id => id.ID_user);
        }
    }
}

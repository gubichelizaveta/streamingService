using course_project.Model;
using course_project.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public int id;
        private MyPlayerDbContext context;
        TracksRepository trackrepository;
        UserRepository userRepository;
        public HomePage()
        {
            InitializeComponent();
            context = new MyPlayerDbContext();
            trackrepository = new TracksRepository(context);
            userRepository = new UserRepository(context);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var users_listt = context.Users.Where(a => a.ID_user == id).Select(a => a.Login).ToList();
            foreach (var name in users_listt)
            {
                forusertextbox.Text = $"Только для тебя, {name}";
            }
            //////
            var liked_tracks = context.LikedTracks.Where(c => c.User_Id.ID_user == id).Select(c => c.Track.Track_Name).Take(1);
            string track_name = "";
            foreach(var i in liked_tracks)
            {
                track_name = i;
            }
            IEnumerable<Tracks> tracks = trackrepository.GetAll();
            foreach(var i in tracks)
            {
                if(i.Track_Name == track_name)
                {
                    Binding binding = new Binding();                  
                    binding.Source = i.Image; 
                    firstimage.SetBinding(Image.SourceProperty, binding);
                    firstname.Text = i.Track_Name;
                }
            }

            string track_name2 = "";
            string track_name3 = "";
            string track_name4 = "";
            string track_name5 = "";
            var liked_tracks5 = context.LikedTracks.Where(c => c.User_Id.ID_user == id).Select(c => c.Track.Track_Name).Take(5);
            foreach(var j in liked_tracks5)
            {
                if (j != track_name)
                {
                    track_name2 = j; break;
                }
            }
            foreach (var i in tracks)
            {
                if (i.Track_Name == track_name2)
                {
                    Binding binding = new Binding();
                    binding.Source = i.Image;
                    secondimage.SetBinding(Image.SourceProperty, binding);
                    secondname.Text = i.Track_Name;
                }
            }
            //
            foreach (var j in liked_tracks5)
            {
                if (j != track_name && j != track_name2)
                {
                    track_name3 = j; break;
                }
            }
            foreach (var i in tracks)
            {
                if (i.Track_Name == track_name3)
                {
                    Binding binding = new Binding();
                    binding.Source = i.Image;
                    thirdimage.SetBinding(Image.SourceProperty, binding);
                    thirdname.Text = i.Track_Name;
                }
            }
            //
            foreach (var j in liked_tracks5)
            {
                if (j != track_name && j!=track_name3 && j!=track_name2)
                {
                    track_name4 = j; break;
                }
            }
            foreach (var i in tracks)
            {
                if (i.Track_Name == track_name4)
                {
                    Binding binding = new Binding();
                    binding.Source = i.Image;
                    fourthimage.SetBinding(Image.SourceProperty, binding);
                    fourthname.Text = i.Track_Name;
                }
            }
            //
            foreach (var j in liked_tracks5)
            {
                if (j != track_name && j != track_name3 && j != track_name2 && j!=track_name4)
                {
                    track_name5 = j; break;
                }
            }
            foreach (var i in tracks)
            {
                if (i.Track_Name == track_name5)
                {
                    Binding binding = new Binding();
                    binding.Source = i.Image;
                    fifthimage.SetBinding(Image.SourceProperty, binding);
                    fifthname.Text = i.Track_Name;
                }
            }

      
            var filtr_artist = context.Filtrs.Where(a => a.User_Id.ID_user == id).Select(b => b.Liked_Artist.Artist_Name).ToList();
            int count_filtr = filtr_artist.Count;
          
            if(count_filtr == 1) //артист один - все треки в рекомендации его
            {
                string artist_name = "";
               foreach(var t in filtr_artist)
               {
                    artist_name = t;
               }
                var all_track_artist = context.Tracks.Where(c => c.Artist == artist_name).ToList();
                
                int i = 0;
                foreach (var j in all_track_artist)
                {
                  
                    if (i == 0) 
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        image1.SetBinding(Image.SourceProperty, binding);
                        filtrartist1.Text = j.Artist;
                        filtrtrack1.Text = j.Track_Name;
                    }
                    if (i == 1)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage2.SetBinding(Image.SourceProperty, binding);
                        artistfiltr2.Text = j.Artist;
                        filtrtrack2.Text = j.Track_Name;
                    }
                    if (i == 2)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage3.SetBinding(Image.SourceProperty, binding);
                        filtrartist3.Text = j.Artist;
                        filtrtrack3.Text = j.Track_Name;
                    }
                    if (i == 3)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage4.SetBinding(Image.SourceProperty, binding);
                        filtrartist4.Text = j.Artist;
                        filtrtrack4.Text = j.Track_Name;
                    }
                    if (i == 4)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage5.SetBinding(Image.SourceProperty, binding);
                        filtrarist5.Text = j.Artist;
                        filtrtrack5.Text = j.Track_Name;
                    }
                    i++;
                }
            }
            //-----если у юзера 2 3 4 5 и больше артистов в фильтре
            if(count_filtr < 5 && count_filtr!=0)
            {
                Random r = new Random();
                int rnd = r.Next(0, count_filtr-1);
                int rnd2 = rnd + 1;
                if (rnd2 > count_filtr) rnd2 = rnd2 - 3;
                string artist1 = "";
                string artist2 = "";
                for (int h = 0; h < count_filtr; h++)
                {
                    if (h == rnd) artist1 = filtr_artist[h];
                    if (h == rnd2) artist2 = filtr_artist[h];
                }

                var all_track_artist1 = context.Tracks.Where(a => a.Artist == artist1).Take(3).ToList();
                var all_track_artist2 = context.Tracks.Where(b => b.Artist == artist2).Take(2).ToList();
                int kl = 0; int ii = 0;
                foreach (var j in all_track_artist1)
                {

                    if (kl == 0)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        image1.SetBinding(Image.SourceProperty, binding);
                        filtrartist1.Text = j.Artist;
                        filtrtrack1.Text = j.Track_Name;
                    }
                    if (kl == 1)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage2.SetBinding(Image.SourceProperty, binding);
                        artistfiltr2.Text = j.Artist;
                        filtrtrack2.Text = j.Track_Name;
                    }
                    if (kl == 2)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage3.SetBinding(Image.SourceProperty, binding);
                        filtrartist3.Text = j.Artist;
                        filtrtrack3.Text = j.Track_Name;
                    }
                    
                   
                    kl++;
                }

                //
                foreach (var j in all_track_artist2)
                {

                    
                    if (ii == 0)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage4.SetBinding(Image.SourceProperty, binding);
                        filtrartist4.Text = j.Artist;
                        filtrtrack4.Text = j.Track_Name;
                    }
                    if (ii == 1)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage5.SetBinding(Image.SourceProperty, binding);
                        filtrarist5.Text = j.Artist;
                        filtrtrack5.Text = j.Track_Name;
                    }
                    ii++;
                }
            }

            if(count_filtr >= 5)
            {
                Random r = new Random();
                int rnd = r.Next(0, count_filtr - 1);
                int rnd2 = rnd + 1;
                if (rnd2 > count_filtr) rnd2 = rnd2 - 3;
                string artist1 = "";
                string artist2 = "";
                for (int h = 0; h < count_filtr; h++)
                {
                    if (h == rnd) artist1 = filtr_artist[h];
                    if (h == rnd2) artist2 = filtr_artist[h];
                }

                var all_track_artist1 = context.Tracks.Where(a => a.Artist == artist1).Take(3).ToList();
                var all_track_artist2 = context.Tracks.Where(b => b.Artist == artist2).Take(2).ToList();
                int kl = 0; int ii = 0;
                foreach (var j in all_track_artist1)
                {

                    if (kl == 0)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        image1.SetBinding(Image.SourceProperty, binding);
                        filtrartist1.Text = j.Artist;
                        filtrtrack1.Text = j.Track_Name;
                    }
                    if (kl == 1)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage2.SetBinding(Image.SourceProperty, binding);
                        artistfiltr2.Text = j.Artist;
                        filtrtrack2.Text = j.Track_Name;
                    }
                    if (kl == 2)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage3.SetBinding(Image.SourceProperty, binding);
                        filtrartist3.Text = j.Artist;
                        filtrtrack3.Text = j.Track_Name;
                    }


                    kl++;
                }

                //
                foreach (var j in all_track_artist2)
                {


                    if (ii == 0)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage4.SetBinding(Image.SourceProperty, binding);
                        filtrartist4.Text = j.Artist;
                        filtrtrack4.Text = j.Track_Name;
                    }
                    if (ii == 1)
                    {
                        Binding binding = new Binding();
                        binding.Source = j.Image;
                        filtrimage5.SetBinding(Image.SourceProperty, binding);
                        filtrarist5.Text = j.Artist;
                        filtrtrack5.Text = j.Track_Name;
                    }
                    ii++;
                }
            }
        }
    }
}

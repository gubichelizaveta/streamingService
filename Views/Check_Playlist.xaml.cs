using course_project.Model;
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
using System.Windows.Shapes;

namespace course_project.Views
{
    /// <summary>
    /// Логика взаимодействия для Check_Playlist.xaml
    /// </summary>
    public partial class Check_Playlist : Window
    {
        string playlist = "";
        public int id;
        private MyPlayerDbContext context;
        public Check_Playlist()
        {
            InitializeComponent();
        }
        public Check_Playlist(string playlist, int id)
        {
            this.playlist = playlist;
            this.id = id;
            InitializeComponent();
            context = new MyPlayerDbContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IEnumerable<Tracks> tracks_in_playlist = context.Playlists.Where(a => a.Playlist_Name == playlist && a.User_Id.ID_user == id).Select(a=>a.Track).ToList();
            checkdatagrid.ItemsSource = tracks_in_playlist;
        }
    }
}

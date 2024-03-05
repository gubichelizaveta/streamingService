using course_project.Model;
using course_project.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для CreatePlaylistPage.xaml
    /// </summary>
    public partial class CreatePlaylistPage : Page
    {
        public int id;
        private MyPlayerDbContext context;
        TracksRepository trackrepository;
        UserRepository userRepository;
        public CreatePlaylistPage()
        {
            InitializeComponent();
            context = new MyPlayerDbContext();
            trackrepository = new TracksRepository(context);
            userRepository = new UserRepository(context);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IEnumerable<Tracks> all_tracks = trackrepository.GetAll();
            trackdatagrid.ItemsSource = all_tracks;
        }
        List<Tracks> tracksforplaylist = new List<Tracks>();
        private void createplaylist_Click(object sender, RoutedEventArgs e) //добавить
        {
            var selected = trackdatagrid.SelectedCells;
            string list = "";

            foreach (var item in selected)
            {
                var content = item.Column.GetCellContent(item.Item);
                var row = (Tracks)content.DataContext;

                list = row.Track_Name;
                break;
            }


                MessageBox.Show(list);


            Tracks track_pl = trackrepository.Get(list);
            tracksforplaylist.Add(track_pl);
        }

        private void createpl_Click(object sender, RoutedEventArgs e) //создать
        {
            string playlist_name = playlistnamebox.Text;
            foreach(var tr in tracksforplaylist)
            {
                Playlists playlists = new Playlists();
                playlists.Playlist_Name = playlist_name;
                playlists.User_Id = userRepository.Get(id);
                playlists.Track = tr;
                context.Playlists.Add(playlists);
                context.SaveChanges();
            }
            tracksforplaylist.Clear();
            playlistnamebox.Clear();
        }
    }
}

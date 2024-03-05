using course_project.Model;
using course_project.ViewModel;
using course_project.Views;
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

namespace course_project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int ID;
        public User USER;
        HomePage home_page = new HomePage();
        SearchPage search_page = new SearchPage();    
        Mediateka_Page mediateka_page = new Mediateka_Page();
        CreatePlaylistPage playlist_page = new CreatePlaylistPage();
        LikedTrackPage liked_tracks_page = new LikedTrackPage();
        FiltrPage filtr_page = new FiltrPage();
        public MainWindow()
        {
            InitializeComponent();
            MyPlayerDbContext context = new MyPlayerDbContext();
            //DataContext = new MainWindowViewModel();  
            frame.Content = home_page;
        }
        public MainWindow(User user)
        {
            USER = user;
            ID = user.ID_user;
            InitializeComponent();
            MyPlayerDbContext context = new MyPlayerDbContext();
            //DataContext = new MainWindowViewModel();
            home_page.id = ID;
            frame.Content = home_page;
        }

        private void Search_MouseDown(object sender, MouseButtonEventArgs e)
        {
            search_page.id = ID;
            search_page.UsEr = USER;
            frame.Content = search_page;
        }

        private void Home_MouseDown(object sender, MouseButtonEventArgs e)
        {
            home_page.id = ID;
            frame.Content = home_page;
        }

        private void Mediateka_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mediateka_page.id = ID;
            frame.Content = mediateka_page;
        }

        private void Playlist_MouseDown(object sender, MouseButtonEventArgs e)
        {
            playlist_page.id = ID;
            frame.Content = playlist_page;
        }

        private void Liked_MouseDown(object sender, MouseButtonEventArgs e)
        {
            liked_tracks_page.id = ID;
            frame.Content = liked_tracks_page;
        }

        private void filtr_Click(object sender, RoutedEventArgs e)
        {
            filtr_page.id = ID;
            frame.Content = filtr_page;
        }
    }
}

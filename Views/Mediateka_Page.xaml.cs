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
    /// Логика взаимодействия для Mediateka_Page.xaml
    /// </summary>
    public partial class Mediateka_Page : Page
    {
        public int id;
        private MyPlayerDbContext context;
        TracksRepository trackrepository;
        PlaylistRepository PlaylistRepository;
        UserRepository userRepository;
        public Mediateka_Page()
        {
            InitializeComponent();
            context = new MyPlayerDbContext();
            trackrepository = new TracksRepository(context);
            userRepository = new UserRepository(context);
            PlaylistRepository = new PlaylistRepository(context);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            wrapplaylist.Children.Clear();
            var users_playlist = context.Playlists.Where(a => a.User_Id.ID_user == id).ToList();
            foreach (var i in users_playlist)
            {
                ListBox listBox = new ListBox();
                listBox.Width = 160;
                listBox.Height = 200;
                //listBox.Margin = new Thickness()
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                //imagesource

                Tracks track = context.Playlists.Where(a => a == i).Select(a => a.Track).FirstOrDefault();
                Image image = new Image();
                image.Width = 96;
                image.Margin = new Thickness(25, 20, 0, 0);
                Binding binding = new Binding();

                binding.Source = track.Image;
                image.SetBinding(Image.SourceProperty, binding);
                TextBlock textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontSize = 22;
                textBlock.Text = i.Playlist_Name;
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textBlock);
                listBox.Items.Add(stackPanel);
                wrapplaylist.Children.Add(listBox);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            int count_listbox = VisualTreeHelper.GetChildrenCount(wrapplaylist);
            List<ListBox> all_listbox = new List<ListBox>();

            for (int i = 0; i < count_listbox; i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(wrapplaylist, i);
                if (childVisual is ListBox listBox)
                {
                    if (listBox.SelectedItem != null)
                    {                       
                            all_listbox.Add(listBox);
                            listBox.SelectedIndex = -1;
                        
                    }
                }
            }
            if (all_listbox.Count == 0)
            {
                MessageBox.Show("Выберите плейлист");
            }
            if (all_listbox.Count == 1)
            {
                string playlist_name = "";
                foreach (var i in all_listbox)
                {
                    var list_items = i.Items;
                    var stackPanel = list_items.GetItemAt(0);
                    
                    if(stackPanel is StackPanel stack)
                    {
                       Visual child = (Visual)VisualTreeHelper.GetChild(stack, 1);
                       if(child is TextBlock text)
                       {
                          playlist_name = text.Text;
                       }
                    }
                  
                }
                Check_Playlist check_Playlist = new Check_Playlist(playlist_name,id);
                check_Playlist.Show();
                //MessageBox.Show(playlist_name);
                //MessageBox.Show(all_listbox.Count.ToString());
            }
            if(all_listbox.Count > 1)
            {
                MessageBox.Show("Выберите один плейлист");
            }
            all_listbox.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int count_listbox = VisualTreeHelper.GetChildrenCount(wrapplaylist);
            List<ListBox> all_listbox = new List<ListBox>();

            for (int i = 0; i < count_listbox; i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(wrapplaylist, i);
                if (childVisual is ListBox listBox)
                {
                    if (listBox.SelectedItem != null)
                    {
                        all_listbox.Add(listBox);
                        listBox.SelectedIndex = -1;

                    }
                }
            }
            if (all_listbox.Count == 0)
            {
                MessageBox.Show("Выберите плейлист");
            }
            if (all_listbox.Count == 1)
            {
                string playlist_name = "";
                foreach (var i in all_listbox)
                {
                    var list_items = i.Items;
                    var stackPanel = list_items.GetItemAt(0);

                    if (stackPanel is StackPanel stack)
                    {
                        Visual child = (Visual)VisualTreeHelper.GetChild(stack, 1);
                        if (child is TextBlock text)
                        {
                            playlist_name = text.Text;
                        }
                    }

                }
                var id_all_playlist = context.Playlists.Where(a => a.Playlist_Name == playlist_name && a.User_Id.ID_user == id).Select(a => a.Playlist_ID).ToList();
                foreach(var id in id_all_playlist)
                {
                    PlaylistRepository.Delete(id);
                    context.SaveChanges();
                }
                Page_Loaded(new object(), new RoutedEventArgs());
            }
            if (all_listbox.Count > 1)
            {
                MessageBox.Show("только один плейлист");
            }
            all_listbox.Clear();
        }
    }
}

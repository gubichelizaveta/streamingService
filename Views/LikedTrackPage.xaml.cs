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
    /// Логика взаимодействия для LikedTrackPage.xaml
    /// </summary>
    public partial class LikedTrackPage : Page
    {
        public int id;
        MyPlayerDbContext context = new MyPlayerDbContext();
        TracksRepository tracksRepository;
        public LikedTrackPage()
        {
            InitializeComponent();
            tracksRepository = new TracksRepository(context);
        }

        StackPanel stackPanel;
        TextBlock textBlock;
        StackPanel stackPanel2;
        Image image;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var users_listt = context.LikedTracks.Where(a => a.User_Id.ID_user == id).Select(a => a.User_Id.Login).ToList();
            foreach(var name in users_listt)
            {
                usernamebox.Text = name.ToString();
            }
            var users_list = context.LikedTracks.Where(a => a.User_Id.ID_user == id).Select(a => a.Track.Track_Name).ToList();
            if (MainStack != null)
            {
                MainStack.Children.Clear();
            }
            foreach (var str in users_list)
            {
                Tracks tracks = tracksRepository.Get(str.ToString());
                //MessageBox.Show($"{str");
                //Tracks tracks = str.Track;

                
                stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Width = 1100; stackPanel.Height = 80;
                stackPanel.Margin = new Thickness(0, 20, 0, 0);
                MainStack.Children.Add(stackPanel);
                textBlock = new TextBlock();
                textBlock.Text = Convert.ToString(tracks.Count);
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Foreground = Brushes.DarkGray;
                textBlock.Margin = new Thickness(20, 0, 0, 0);
                stackPanel.Children.Add(textBlock);
                stackPanel2 = new StackPanel();
                stackPanel2.Orientation = Orientation.Horizontal;
                stackPanel2.Width = 320;
                stackPanel2.Margin = new Thickness(20, 0, 0, 0);
                stackPanel.Children.Add(stackPanel2);

                image = new Image();
                image.Width = 64; image.Height = 64;
                image.VerticalAlignment = VerticalAlignment.Center;
                image.Margin = new Thickness(20, 0, 0, 0);
                Binding binding = new Binding();
                //binding.ElementName = "Image";
                binding.Source = tracks.Image;
                //binding.Path = new PropertyPath("Source");
                image.SetBinding(Image.SourceProperty, binding);
                stackPanel2.Children.Add(image);
                StackPanel stackPanel3 = new StackPanel();
                stackPanel2.Children.Add(stackPanel3);

                TextBlock textBlock2 = new TextBlock();
                textBlock2.FontSize = 20;
                textBlock2.Text = tracks.Track_Name;
                textBlock2.Width = 220;
                textBlock2.Margin = new Thickness(10, 20, 0, 0);
                TextBlock textBlock3 = new TextBlock();
                textBlock3.Text = tracks.Artist;
                textBlock3.Margin = new Thickness(10, 0, 0, 0);
                stackPanel3.Children.Add(textBlock2);
                stackPanel3.Children.Add(textBlock3);
                //Binding binding = new Binding();
                //binding.Source = track.Image;
                //image.SetBinding(Image.SourceProperty.binding);

                TextBlock textBlock4 = new TextBlock();
                textBlock4.FontSize = 20;
                textBlock4.Text = tracks.Album_Name;
                textBlock4.Width = 283;
                textBlock4.Margin = new Thickness(80, 27, 0, 27);
                textBlock4.VerticalAlignment = VerticalAlignment.Center;
                //
                StackPanel stackartist = new StackPanel();
                stackartist.Width = 200;
                stackartist.VerticalAlignment = VerticalAlignment.Center;
                stackartist.Margin = new Thickness(28, 0, 0, 0);
                stackPanel.Children.Add(stackartist);
                //

                TextBlock textBlock5 = new TextBlock();
                textBlock5.FontSize = 20;
                textBlock5.Text = tracks.Artist;
                textBlock5.VerticalAlignment = VerticalAlignment.Center;
                textBlock5.Margin = new Thickness(10, 0, 0, 0);
                stackPanel.Children.Add(textBlock4);
                stackartist.Children.Add(textBlock5);

                Image image2 = new Image();
                image2.Width = 32; image2.Height = 32;
                image2.Margin = new Thickness(50, 0, 0, 0);

                Binding binding2 = new Binding();
                binding2.Source = "/Views/delete.png";

                image2.SetBinding(Image.SourceProperty, binding2);
                image2.MouseLeave += mouse_leave;
                image2.MouseEnter += mouse_enter;
                image2.MouseDown += mouse_down;
                image2.Opacity = tracks.Count;
                stackPanel.Children.Add(image2);
                if (tracks.Count > 6) MainStack.Height = 540 + (tracks.Count - 6) * 100;
            }
        }

        private void mouse_down(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            int opacity = Convert.ToInt32(img.Opacity.ToString()); //тут номер трека
            IEnumerable<Tracks> trackss = tracksRepository.GetAll();
            string track_name = "";
            foreach (Tracks tr in trackss)
            {
                if (tr.Count == opacity)
                {
                    track_name = tr.Track_Name;
                }
            }
            var lik_tr = context.LikedTracks.Where(c => c.Track.Track_Name == track_name && c.User_Id.ID_user == id).Select(c => c.List_ID).ToList();
            LikedTracks liked = new LikedTracks();
            foreach(var i in lik_tr)
            {
                liked = context.LikedTracks.Find(i);
            }
            if (liked!=null) context.LikedTracks.Remove(liked);
            context.SaveChanges();
            Page_Loaded(new object(), new RoutedEventArgs());
        }
        private void mouse_leave(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Width = 32; img.Height = 32;
        }

        private void mouse_enter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            img.Width = 36; img.Height = 36;
        }

    }
}

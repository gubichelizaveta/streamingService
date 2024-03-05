using course_project.Model;
using course_project.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        
        int number_track = 1;
        public int id;
        public User UsEr;
        private MyPlayerDbContext context;
        TracksRepository trackrepository;
        UserRepository userRepository;
        Tracks track_add = new Tracks();
      
        #region макет для треков
        StackPanel stackPanel;
        TextBlock textBlock;
        StackPanel stackPanel2;
        Image image;
   
        #endregion
        public SearchPage()
        {
            InitializeComponent();
            context = new MyPlayerDbContext();
            trackrepository = new TracksRepository(context);
            userRepository = new UserRepository(context);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (number_track == 1)
            {
                IEnumerable<Tracks> tracks = trackrepository.GetAll();
                foreach (Tracks track in tracks)
                {
                    ///////имя артиста и картинка !!!
                    stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Width = 1100; stackPanel.Height = 80;
                    stackPanel.Margin = new Thickness(0, 20, 0, 0);
                    MainStack.Children.Add(stackPanel);
                    textBlock = new TextBlock();
                    textBlock.Text = Convert.ToString(track.Count);
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
                    binding.Source = track.Image;
                    //binding.Path = new PropertyPath("Source");
                    image.SetBinding(Image.SourceProperty, binding);
                    stackPanel2.Children.Add(image);
                    StackPanel stackPanel3 = new StackPanel();
                    stackPanel2.Children.Add(stackPanel3);

                    TextBlock textBlock2 = new TextBlock();
                    textBlock2.FontSize = 20;
                    textBlock2.Text = track.Track_Name;
                    textBlock2.Width = 220;
                    textBlock2.Margin = new Thickness(10, 20, 0, 0);
                    TextBlock textBlock3 = new TextBlock();
                    textBlock3.Text = track.Artist;
                    textBlock3.Margin = new Thickness(10, 0, 0, 0);
                    stackPanel3.Children.Add(textBlock2);
                    stackPanel3.Children.Add(textBlock3);
                    //Binding binding = new Binding();
                    //binding.Source = track.Image;
                    //image.SetBinding(Image.SourceProperty.binding);
                  
                    TextBlock textBlock4 = new TextBlock();
                    textBlock4.FontSize = 20;
                    textBlock4.Text = track.Album_Name;
                    textBlock4.Width = 283;
                    textBlock4.Margin = new Thickness(48, 27, 0, 27);
                    textBlock4.VerticalAlignment = VerticalAlignment.Center;
                    //
                    StackPanel stackartist = new StackPanel();
                    stackartist.Width = 200;
                    stackartist.VerticalAlignment = VerticalAlignment.Center;
                    stackartist.Margin = new Thickness(48, 0, 0, 0);
                    stackPanel.Children.Add(stackartist);
                    //
                    
                    TextBlock textBlock5 = new TextBlock();
                    textBlock5.FontSize = 20;
                    textBlock5.Text = track.Artist;
                    textBlock5.VerticalAlignment = VerticalAlignment.Center;
                    textBlock5.Margin = new Thickness(20, 0, 0, 0);
                    stackPanel.Children.Add(textBlock4);
                    stackartist.Children.Add(textBlock5);
                    Image image2 = new Image();
                    image2.Width = 32; image2.Height = 32;
                    image2.Margin = new Thickness(50, 0, 0, 0);
                    
                    Binding binding2 = new Binding();
                    binding2.Source = "/Views/addheart.png";
                   
                    image2.SetBinding(Image.SourceProperty, binding2);
                    image2.MouseLeave += mouse_leave;
                    image2.MouseEnter += mouse_enter;
                    image2.MouseDown += mouse_down;
                    image2.Opacity = track.Count;
                    stackPanel.Children.Add(image2);
                    if (number_track > 6) MainStack.Height = 850 + (number_track - 6) * 100;
                    number_track++;


                }
            }
        }

        private void mouse_down(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            int opacity = Convert.ToInt32(img.Opacity.ToString()); //тут номер трека
            //получить по номеру трека название трека. получить все добавленные треки конкретного пользователя и проверить нет ли уже такого трека
            //если есть то вывести уведомление что трек уже добавлен иначе добавлять трек юзеру
            IEnumerable<Tracks> trackss = trackrepository.GetAll();
            Tracks trck = new Tracks();
            string track_name = "";
            foreach(Tracks tr in trackss)
            {
                if(tr.Count == opacity)
                {
                    track_name = tr.Track_Name;
                    trck = tr;
                }
            }
            IEnumerable<LikedTracks> likedTracks = context.LikedTracks.AsNoTracking().ToList();
    
            var lik_tr = context.LikedTracks.Where(c => c.Track.Track_Name == track_name && c.User_Id.ID_user == id).ToList();
            

            if (lik_tr.Count != 0)
            {
                MessageBox.Show("Трек УЖЕ в избранном");
            }

            else
            {
                LikedTracks like = new LikedTracks();
                like.User_Id = userRepository.Get(id);
                track_add = trackrepository.Get(track_name);
                like.Track = track_add;
              
                context.LikedTracks.Add(like);
                context.SaveChanges();
                MessageBox.Show("Трек добавлен в избранное");
            }

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


        //private void image_focus(object sender, RoutedEventHandler e)
        //{
        //    image2.Visibility = Visibility.Visible;
        //}
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            number_track = 1;
            var search = searchbox.Text;
            if (e.Key == Key.Enter)
            {
                MainStack.Children.Clear();
                IEnumerable<Tracks> tracks = trackrepository.GetAll();
                if (number_track == 1)
                {
                    foreach (Tracks track in tracks)
                    {
                        if (Regex.IsMatch(track.Track_Name, $@"{search}(\w*)", RegexOptions.IgnoreCase))
                        {
                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Orientation = Orientation.Horizontal;
                            stackPanel.Width = 1100; stackPanel.Height = 80;
                            stackPanel.Margin = new Thickness(0, 20, 0, 0);
                            MainStack.Children.Add(stackPanel);
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = track.Count.ToString();
                            textBlock.VerticalAlignment = VerticalAlignment.Center;
                            textBlock.Foreground = Brushes.DarkGray;
                            textBlock.Margin = new Thickness(20, 0, 0, 0);
                            stackPanel.Children.Add(textBlock);
                            StackPanel stackPanel2 = new StackPanel();
                            stackPanel2.Orientation = Orientation.Horizontal;
                            stackPanel2.Width = 320;
                            stackPanel2.Margin = new Thickness(20, 0, 0, 0);
                            stackPanel.Children.Add(stackPanel2);

                            Image image = new Image();
                            image.Width = 64; image.Height = 64;
                            image.VerticalAlignment = VerticalAlignment.Center;
                            image.Margin = new Thickness(20, 0, 0, 0);
                            Binding binding = new Binding();
                            //binding.ElementName = "Image";
                            binding.Source = track.Image;
                            //binding.Path = new PropertyPath("Source");
                            image.SetBinding(Image.SourceProperty, binding);
                            stackPanel2.Children.Add(image);
                            StackPanel stackPanel3 = new StackPanel();
                            stackPanel2.Children.Add(stackPanel3);

                            TextBlock textBlock2 = new TextBlock();
                            textBlock2.FontSize = 20;
                            textBlock2.Text = track.Track_Name;
                            textBlock2.Width = 220;
                            textBlock2.Margin = new Thickness(10, 20, 0, 0);
                            TextBlock textBlock3 = new TextBlock();
                            textBlock3.Text = track.Artist;
                            textBlock3.Margin = new Thickness(10, 0, 0, 0);
                            stackPanel3.Children.Add(textBlock2);
                            stackPanel3.Children.Add(textBlock3);
                            //Binding binding = new Binding();
                            //binding.Source = track.Image;
                            //image.SetBinding(Image.SourceProperty.binding);


                            TextBlock textBlock4 = new TextBlock();
                            textBlock4.FontSize = 20;
                            textBlock4.Text = track.Album_Name;
                            textBlock4.Width = 283;
                            textBlock4.Margin = new Thickness(48, 27, 0, 27);
                            textBlock4.VerticalAlignment = VerticalAlignment.Center;
                            //
                            StackPanel stackartist = new StackPanel();
                            stackartist.Width = 200;
                            stackartist.VerticalAlignment = VerticalAlignment.Center;
                            stackartist.Margin = new Thickness(48, 0, 0, 0);
                            stackPanel.Children.Add(stackartist);

                            TextBlock textBlock5 = new TextBlock();
                            textBlock5.FontSize = 20;
                            textBlock5.Text = track.Artist;
                            textBlock5.VerticalAlignment = VerticalAlignment.Center;
                            textBlock5.Margin = new Thickness(48, 0, 0, 0);
                            stackPanel.Children.Add(textBlock4);
                            stackartist.Children.Add(textBlock5);
                            Image image2 = new Image();
                            image2.Width = 32; image2.Height = 32;
                            image2.Margin = new Thickness(50, 0, 0, 0);

                            Binding binding2 = new Binding();
                            binding2.Source = "/Views/addheart.png";

                            image2.SetBinding(Image.SourceProperty, binding2);
                            image2.MouseLeave += mouse_leave;
                            image2.MouseEnter += mouse_enter;
                            image2.MouseDown += mouse_down;
                            image2.Opacity = track.Count;
                            stackPanel.Children.Add(image2);
                            if (number_track > 1) MainStack.Height = 1850; /*+ (number_track - 6) * 100;*/
                            number_track++;
                        }
                    }
                }
            }
        }

        private void Image_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
    /// Логика взаимодействия для FiltrPage.xaml
    /// </summary>
    public partial class FiltrPage : Page
    {
        public int id;
        private MyPlayerDbContext context;
        UserRepository userRepository;
        MusicianRepositories musicianRepositories;
        public FiltrPage()
        {
            InitializeComponent();
            context = new MyPlayerDbContext();
            userRepository = new UserRepository(context);
            musicianRepositories = new MusicianRepositories(context);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var users_filtr = context.Filtrs.Where(a => a.User_Id.ID_user == id).Select(a => a.Liked_Artist.Artist_Image).ToList();
            int count_ellipse = VisualTreeHelper.GetChildrenCount(wrap);
            List<Ellipse> all_ellipse = new List<Ellipse>();

            for (int i = 0; i < count_ellipse; i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(wrap, i);
                if (childVisual is Ellipse ellipse)
                {
                    all_ellipse.Add(ellipse);
                }            
            }

            for (int j = 0; j < users_filtr.Count; j++)
            {
                
                ImageBrush imageBrush = new ImageBrush();
                ImageSource image = new BitmapImage(new Uri(users_filtr[j]));
                imageBrush.ImageSource = image;
                all_ellipse[j].Fill = imageBrush;
            }

            for (int count = users_filtr.Count; count < all_ellipse.Count; count++)
            {
                if (all_ellipse[count] != null)
                {
                    all_ellipse[count].Fill = null;
                }
            }
        }

        private void submitbutton_Click(object sender, RoutedEventArgs e)
        {
            var filtrdelete = context.Filtrs.Where(a => a.User_Id.ID_user == id).Select(b => b.Filtr_ID).ToList();
            foreach(var t in filtrdelete)
            {
                
                ForFiltr filtr = new ForFiltr();
                filtr = context.Filtrs.Find(t);
                context.Filtrs.Remove(filtr);
                context.SaveChanges();
            }

            int count_panel = VisualTreeHelper.GetChildrenCount(MainWrap);
            List<StackPanel> all_stackpanel = new List<StackPanel>();
            List<CheckBox> all_checkbox = new List<CheckBox>();
            int count_checkbox;
            for (int i = 0; i < count_panel; i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(MainWrap, i);
                if (childVisual is StackPanel stackPanel)
                {
                    count_checkbox = VisualTreeHelper.GetChildrenCount(stackPanel);
                    for (int j = 0; j < count_checkbox; j++)
                    {
                        Visual StackchildVisual = (Visual)VisualTreeHelper.GetChild(stackPanel, j);
                        if(StackchildVisual is CheckBox check)
                        {

                            if (check.IsChecked == true)
                            {
                                
                              all_checkbox.Add(check);
                            }
                        }
                    }
                }
            }

            foreach(var g in all_checkbox)
            {
                ForFiltr forFiltr = new ForFiltr();
                forFiltr.User_Id = userRepository.Get(id);
                forFiltr.Liked_Artist = musicianRepositories.Get(Convert.ToString(g.Content));
                //forFiltr.Liked_Artist
                context.Filtrs.Add(forFiltr);
                context.SaveChanges();
            }
            MessageBox.Show("Фильтр настроен успешно");
            foreach(var check in all_checkbox)
            {
                check.IsChecked = false;
            }
            Page_Loaded(new object(), new RoutedEventArgs());
        }
    }
}

using course_project.ViewModel;
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
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>

    public partial class Autorization : Window
    {
        AutorizationPage autorizationPage = new AutorizationPage();
        RegistrationPage registrationPage = new RegistrationPage();
        public Autorization()
        {
            InitializeComponent();
            autorization.Content = autorizationPage;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
       
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            autorization.Content = autorizationPage;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            autorization.Content = registrationPage;
        }
    }
}

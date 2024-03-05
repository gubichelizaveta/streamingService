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
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        MyPlayerDbContext db = new MyPlayerDbContext();
        
        public AutorizationPage()
        {
            InitializeComponent();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            //! закрыть окно авторизации
            var passwordBox = PasswordBox as PasswordBox;
            var password = passwordBox.Password;
            //var password = Encryption.Encrypt(PasswordBox.Password);
            var login = LoginBox.Text;
            UserRepository userRepository = new UserRepository(db);
            if (Validation.IsLoginValid(login) && Validation.IsPasswordValid(password)) 
            {
                bool authorization = false;               
                IEnumerable<User> users = userRepository.GetAll();               
                foreach (User user in users)
                {                   
                    if (user.Login == login && user.Password == Encryption.Encrypt(password))
                    {
                        
                        authorization = true;
                        new MainWindow(user).Show();
                        //var window = AutorizationPage.GetFlowDirection(Parent);
                        //Autorization.Close();             
                    }
                }
                if (authorization == false) MessageBox.Show("Пользователя с такими данными не существуют!");
            }
            else
            {
                MessageBox.Show("Логин или пароль введены некорректно!");
            }
        }
    }
}

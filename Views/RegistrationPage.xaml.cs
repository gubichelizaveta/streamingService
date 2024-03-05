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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void registration(object sender, RoutedEventArgs e)
        {
            var login = loginbox.Text;
            var password = passwordbox.Password;
            var repeatpassword = passwordrepeat.Password;
            var username = usernamebox.Text;
            var useremail = useremailbox.Text;
            MyPlayerDbContext myPlayerDbContext = new MyPlayerDbContext();
            UserRepository userRepository = new UserRepository(myPlayerDbContext);

            if (Validation.IsLoginValid(login) && Validation.IsPasswordValid(password))
            {
               
                var userExistsFlag = false;
                IEnumerable<User> users = userRepository.GetAll();
                foreach (User user in users)
                {
                    if (userExistsFlag == true) break;
                    if (user.Login == login)
                    {
                        userExistsFlag = true;
                        MessageBox.Show("Пользователь с таким логином уже существует!");
                    }
                }
                if (userExistsFlag == false)
                {
                    if (password != repeatpassword)
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                    else
                    {
                        User user = new User();
                        //user.ID_user = 2;
                        user.Login = login;
                        user.Password = Encryption.Encrypt(password);
                        user.Name = username;
                        user.Email = useremail;
                        userRepository.Create(user);
                        loginbox.Clear(); passwordbox.Clear(); passwordrepeat.Clear(); useremailbox.Clear(); usernamebox.Clear();
                        MessageBox.Show("Ваш аккаунт был успешно создан! Перейдите на страницу Авторизация");
                    }
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль введены некорректно! Логин должен содержать хотя бы одну цифру, длина логина в пределах от 3 до 20 символов! Пароль должен содержать символ в верхнем регистре,в нижнем и цфиры, длина не меньше 4");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Model
{
    public class User
    {
        private int id_user;
        public int ID_user
        {
            get { return id_user; }
            set { id_user = value; }
        }
        [Key]
        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public ICollection<ForFiltr> user_filtr { get; set; }
        public ICollection<Playlists> user_playlists { get; set; }//вопросик
        public ICollection<LikedTracks> user_like { get; set; }

        //private string country;
        //public string Country
        //{
        //    get { return country; }
        //    set { country = value; }
        //}
    }
}

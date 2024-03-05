using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Model
{
    public class ForFiltr
    {
        [Key]
        private int filtr_Id;
        public int Filtr_ID
        {
            get { return filtr_Id; }
            set { filtr_Id = value; }
        }
        private User user_id;
        public User User_Id
        {
            get { return user_id; }
            set { user_id = value; }
        }
        private Musician liked_artist;
        public Musician Liked_Artist
        {
            get { return liked_artist; }
            set { liked_artist = value; }
        }
    }
}

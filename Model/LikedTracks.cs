using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Model
{
    public class LikedTracks
    {
        [Key]
        private int list_id;
        public int List_ID
        {
            get { return list_id; }
            set { list_id = value; }
        }

        private User user_id;
        public User User_Id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        private Tracks track;
        public Tracks Track
        {
            get { return track; }
            set { track = value; }
        }

    }
}

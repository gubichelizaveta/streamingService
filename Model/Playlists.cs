 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Model
{
    public class Playlists
    {
        [Key]
        private int playlist_id;
        public int Playlist_ID
        {
            get { return playlist_id; }
            set { playlist_id = value; }
        }

        private string playlist_name;
        public string Playlist_Name
        {
            get { return playlist_name; }
            set { playlist_name = value; }
        }

        private string opisanie;
        public string Opisanie
        {
            get { return opisanie; }
            set { opisanie = value; }
        }
        public  int user { get; set; }
        private User user_id;
        public User User_Id
        {
            get { return user_id; }
            set { user_id = value; }
        }
        public string user_track { get; set; }
        private Tracks track;
        public Tracks Track
        {
            get { return track; }
            set { track = value; }
        }

    }
}

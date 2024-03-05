using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Model
{
    public class Tracks
    {
        [Key]
        private string track_name;
        public string Track_Name
        {
            get { return track_name; }
            set { track_name = value; }
        }
        private string artist;
        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }
        private string album_name;
        public string Album_Name
        {
            get { return album_name; }
            set { album_name = value; }
        }
        private string url;
        public string URL
        {
            get { return url; }
            set { url = value; }
        }
        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        private int count { get; set; }
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        public ICollection<LikedTracks> likedTracks {get; set;}
        public ICollection<Playlists> tracksinplaylist { get; set; }
        //АЛЬБОМ
    }
}

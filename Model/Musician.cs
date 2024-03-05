using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_project.Model
{
    public class Musician
    {
        [Key]
        private string artist_name;
        public string Artist_Name
        {
            get { return artist_name; }
            set { artist_name = value; }
        }
        //public ICollection<Tracks> tracks { get; set; }
        private string artist_image;
        public string Artist_Image
        {
            get { return artist_image; }
            set { artist_image = value; }
        }
        public ICollection<ForFiltr> favoriteartist { get; set; }

    }
}

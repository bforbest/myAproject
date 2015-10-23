using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using webpagetest.Models;

namespace myimportantproject.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        [Required()]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }
        public virtual ICollection<Video> videos { get; set; }
    }
}
using myimportantproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webpagetest.Models;

namespace myimportantproject.ViewModel
{
    public class VideoPlayList
    {
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Playlist> Playlists { get; set; }
    }
}
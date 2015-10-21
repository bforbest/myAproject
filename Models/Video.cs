using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webpagetest.Models
{
    public class Video
    {
        public int VideoID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The length should be between 2 too 100", MinimumLength = 2)]
        public string Name { get; set; }
        public bool IsEmbed { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 2)]
        public string VideoUrl { get; set; }

        public string Discription { get; set; }
        [StringLength(300)]
        public string ImageUrl { get; set; }
        public int thumbsUp { get; set; }
        public int thumbsDown { get;set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

    }
}
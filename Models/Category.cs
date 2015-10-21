using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webpagetest.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required()]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }
        public virtual ICollection<Video> videos { get; set; }

    }
}
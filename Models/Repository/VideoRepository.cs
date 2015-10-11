using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webpagetest.Models.Repository
{
    public class VideoRepository : Repository<Video>
    {

        public Video GetByName(string name)
        {
            return DbSet.Where(a => a.Name.Contains(name)).First();
        }
    }
}
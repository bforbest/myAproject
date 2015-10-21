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
        public void thumbsUp(string name)
        {
            Video vid = DbSet.Where(a => a.Name.Contains(name)).First();
            vid.thumbsUp = vid.thumbsUp + 1;
            
        }
        public int thumbsUpCount(string name)
        {
            Video vid = DbSet.Where(a => a.Name.Contains(name)).First();
            int up = vid.thumbsUp;
           
            return up;
        }
        public void thumbsDown(string name)
        {
            Video vid = DbSet.Where(a => a.Name.Contains(name)).First();
            vid.thumbsDown = vid.thumbsDown + 1;

        }
        public int thumbsDownCount(string name)
        {
            Video vid = DbSet.Where(a => a.Name.Contains(name)).First();
            int down = vid.thumbsDown;
           
            return down;
        }
        

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webpagetest.Models;
using webpagetest.Models.Repository;

namespace myimportantproject.Models.Repository
{
    public class CategoryRepository : Repository<Category>
    {
        public Category GetByName(string name)
        {
            return DbSet.Where(a => a.Title.Contains(name)).First();
        }
        public void AddVideoToCategory(Video video, Category category)
        {
            category.videos.Add(video);
        }
    }
}
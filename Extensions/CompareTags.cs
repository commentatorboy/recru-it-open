using recru_it.Data;
using recru_it.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recru_it.Extensions
{
    public class CompareTags
    {
        private readonly ApplicationDbContext _context;

        //Algorithm: Find all jobposts by 1 or more tags
        public static List<JobPost> CompareTagsWithJobPost(List<Tag> tags)
        {
            CompareTags ct = new CompareTags();
            ct._context.JobPosts.Select(jp => tags.Where(t => t.Title == jp.Tag.Title).Single());
            return null;
        }
    }
}

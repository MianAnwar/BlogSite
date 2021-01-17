using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Models
{
    public class PostView
    {
        public List<Post> posts = new List<Post>();
        public PostView()
        {
            posts.Add(new Post { Username = "Anas", Title = "Nikal", Content = "BSSS", Date = System.DateTime.Now });
            posts.Add(new Post { Username = "Anas", Title = "Nikal", Content = "BSSS", Date = System.DateTime.Now });
        }
    }
}

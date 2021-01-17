using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Models
{
    public class Post
    {
        public string Title { get; set; }
        public int? Id { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }
}

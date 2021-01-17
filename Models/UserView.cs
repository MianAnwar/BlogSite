using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Models
{
    public class UserView
    {
        public List<User> users;
        public UserView()
        {
            users = new List<User>();
        }
    }
}

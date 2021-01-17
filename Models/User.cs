using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSite.Models
{
    public class User
    {
        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(50)]
        public string Name { get; set; }
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please enter your age")]
        [Range(1, 100)]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Please enter your username")]
        [StringLength(20)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(20)]
        public string Password { get; set; }

    }
}

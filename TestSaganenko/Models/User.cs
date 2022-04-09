using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TestSaganenko.Models
{
    public class User : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}

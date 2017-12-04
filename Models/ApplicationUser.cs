using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace recru_it.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual JobApplication JobApplication { get; set; }
        public virtual JobPost JobPost { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Company Company { get; set; }



    }
}

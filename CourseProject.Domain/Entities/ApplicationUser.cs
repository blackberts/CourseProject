using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? Created_At { get; set; }
        public DateTime? Last_login { get; set; }
        public string? Status { get; set; }
        public string? Role { get; set; }

        public List<Collection>? Collections { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}

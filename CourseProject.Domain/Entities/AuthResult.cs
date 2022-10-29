using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Domain.Entities
{
    public class AuthResult
    {
        public bool Result { get; set; }
        public string? Error { get; set; }
        public string? UserId { get; set; }
    }
}

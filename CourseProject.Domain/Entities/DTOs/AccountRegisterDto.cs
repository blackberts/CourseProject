using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Domain.Entities.DTOs
{
    public class AccountRegisterDto
    {
        [Display(Name = "User name")]
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string? Email { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Domain.Entities
{
    public class Collection
    {
        public Guid CollectionId { get; set; }
        public string? Name { get; set; }
        public string? Owner { get; set; }
        public string? Description { get; set; }
        public string? Theme { get; set; }
        public byte[]? ImageData { get; set; }
        public DateTime CreatedTime { get; set; }

        // Navigation
        public List<Item>? Items { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<ApplicationUser>? Users { get; set; }
  
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePath { get; set; }
    }
}

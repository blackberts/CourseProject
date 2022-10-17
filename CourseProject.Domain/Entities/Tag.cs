using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Domain.Entities
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string? Name { get; set; }
        
        public List<Item> Items { get; set; }
    }
}

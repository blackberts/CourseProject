using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Domain.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        public Item? Item { get; set; }
        public ApplicationUser? User { get; set; }
    }
}

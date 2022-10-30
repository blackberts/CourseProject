using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Domain.Entities
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public string? Name { get; set; }
        public int? Likes { get; set; }
        public List<string>? UsersWhoLiked { get; set; }


        public List<Comment>? Comments { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<Collection>? Collections { get; }
    }
}

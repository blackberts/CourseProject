using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataContext.Repositories
{
    public interface ITagsRepository
    {
        Task<List<Tag>> GetAll();
        Task<Tag> GetTagById(Guid id);
        Task<Tag> AddTag(Tag tag);
        Task<Tag> EditTag(Tag tag);
        void DeleteTagById(Guid id);
    }
}

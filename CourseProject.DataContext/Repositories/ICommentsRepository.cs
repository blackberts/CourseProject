using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataContext.Repositories
{
    public interface ICommentsRepository
    {
        Task<List<Comment>> GetCommentsForParticularItem(Item item);
    }
}

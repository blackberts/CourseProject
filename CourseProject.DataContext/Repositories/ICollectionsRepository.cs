using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataContext.Repositories
{
    public interface ICollectionsRepository
    {
        Task<List<Collection>> GetAll();
        Task<Collection> GetCollectionById(Guid id);
        Task<Collection> AddCollection(Collection item);
        Task<Collection> EditCollection(Collection item);
        void DeleteCollectionById(Guid id);
    }
}

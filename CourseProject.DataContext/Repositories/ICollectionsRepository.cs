using CourseProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
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
        Task<Collection> AddCollection(Collection collection);
        Task<Collection> EditCollection(Collection collection);
        void DeleteCollectionById(Guid id);
    }
}

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
        Task<ApplicationUser> GetAll(string name);
        Task<Collection> GetCollectionById(Guid id);
        Task<List<Collection>> GetCollectionsForParticularUser(ApplicationUser user);
        Task<List<Collection>> GetCollectionForParticularItem(Item item);
        Task<List<Collection>> GetTheBiggestCollections();
        Task<Collection> AddCollection(Collection collection);
        Task<Collection> EditCollection(Collection collection);
        Task<Collection> AddItemToCollection(Collection collection, Item item, List<string> tags);
        Task<Collection> RemoveItemFromCollection(Item item);
        void DeleteCollectionById(Guid id);
    }
}

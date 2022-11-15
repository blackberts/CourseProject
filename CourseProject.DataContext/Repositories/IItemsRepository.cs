using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataContext.Repositories
{
    public interface IItemsRepository
    {
        Task<List<Item>> GetAll();
        Task<Item> GetItemById(Guid id);
        Task<List<Item>> GetItemsForParticularCollection(Collection collection);
        Task<Item> AddItem(Item item);
        Task<Item> EditItem(Item item);
        Task<Item> AddComment(ApplicationUser user, Item item, Comment comment);
        Task<Item> DeleteComment(Guid id);
        Task<Item> AddLike(Guid itemId, string userId);
        void DeleteItemById(Guid id);
    }
}

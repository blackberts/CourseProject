using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Service
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Item> AddItem(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public void DeleteItemById(Guid id)
        {
            var item = _context.Items.Find(id);

            if(item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(item));
            }
        }

        public async Task<List<Item>> GetAll()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemById(Guid id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<List<Item>> GetItemsForParticularCollection(Collection collection)
        {
            return await _context.Items
                .Where(c => c.Collections
                .Contains(collection))
                .Include(i => i.Collections)
                .Include(i => i.Tags)
                .ToListAsync();
        }

        public async Task<Item> EditItem(Item item)
        {
            var updateItem = await _context.Items.FindAsync(item.ItemId);

            if(updateItem != null)
            {
                updateItem.Name = item.Name;
                updateItem.Tags = item.Tags;

                await _context.SaveChangesAsync();

                return item;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<Item> AddComment(ApplicationUser user, Item item, Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            var userFromDb = await _context.Users
                .Where(u => u.Id == user.Id)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync();

            var itemFromDb = await _context.Items
                .Where(i => i.ItemId == item.ItemId)
                .Include(i => i.Comments)
                .Include(i => i.Collections)
                .Include(i => i.Tags)
                .FirstOrDefaultAsync();

            if(userFromDb != null && itemFromDb != null)
            {
                userFromDb.Comments.Add(comment);
                itemFromDb.Comments.Add(comment);
                
                comment.Item = itemFromDb;
                comment.User = userFromDb;
            }
            await _context.SaveChangesAsync();

            return itemFromDb;
        }

        public async Task<Item> DeleteComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            var itemFromDb = await _context.Items
                .Where(i => i.Comments.Contains(comment))
                .Include(i => i.Comments)
                .FirstOrDefaultAsync();

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }

            return itemFromDb;
        }

        public async Task<Item> AddLike(Guid itemId, Guid userId)
        {
            var item = await _context.Items.FindAsync(itemId);
            if(item.Likes == null && item.UsersWhoLiked == null)
            {
                item.Likes = 0;
                item.UsersWhoLiked = new();
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }
            var user = await _context.Users.FindAsync(userId.ToString());

            if (item.UsersWhoLiked.Contains(user.Id))
            {
                item.UsersWhoLiked.Remove(user.Id);
                item.Likes -= 1;

            }
            else
            {
                item.UsersWhoLiked.Add(user.Id);
                item.Likes += 1;
            }

            await _context.SaveChangesAsync();

            return item;
        }
    }
}

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
    }
}

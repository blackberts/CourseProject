using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Service
{
    public class CollectionsRepository : ICollectionsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public CollectionsRepository(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<Collection> AddCollection(Collection collection)
        {
            if(collection.Image != null)
            {
                string path = "/Images/Collections/" + collection.ImageName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await collection.Image.CopyToAsync(fileStream);
                }
                Collection newCollection = new Collection()
                {
                    Name = collection.Name,
                    Owner = collection.Owner,
                    Description = collection.Description,
                    Theme = collection.Theme,
                    ImageName = collection.ImageName,
                    ImagePath = path
                };
                await _context.Collections.AddAsync(newCollection);
                await _context.SaveChangesAsync();
            }

            return collection;
        }

        public void DeleteCollectionById(Guid id)
        {
            var collection = _context.Collections.Find(id);

            if (collection != null)
            {
                _context.Collections.Remove(collection);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(collection));
            }
        }

        public async Task<List<Collection>> GetAll()
        {
            return await _context.Collections.ToListAsync();
        }

        public async Task<Collection> GetCollectionById(Guid id)
        {
            var collection = await _context.Collections.FindAsync(id);

            if (collection != null)
            {
                return collection;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<Collection> EditCollection(Collection collection)
        {
            var updateCollection = await _context.Collections.FindAsync(collection.CollectionId);

            if (updateCollection != null)
            {
                updateCollection.Name = collection.Name;
                updateCollection.Theme = collection.Theme;
                //updateCollection.Image = collection.Image;
                updateCollection.Description = collection.Description;
                updateCollection.Owner = collection.Owner;

                await _context.SaveChangesAsync();

                return collection;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}

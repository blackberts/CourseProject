using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                byte[] imageData = null;
                using(var binaryReader = new BinaryReader(collection.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)collection.Image.Length);
                }
                collection.ImageData = imageData;

                Collection newCollection = new Collection()
                {
                    CollectionId = Guid.NewGuid(),
                    Name = collection.Name,
                    Owner = collection.Owner,
                    Description = collection.Description,
                    Theme = collection.Theme,
                    Image = collection.Image,
                    ImageData = collection.ImageData,
                };
                await _context.Collections.AddAsync(newCollection);
                await _context.SaveChangesAsync();
            }
            else
            {
                string path = "/images/Collections/Empty.png";

                Collection newCollection = new Collection()
                {
                    CollectionId = Guid.NewGuid(),
                    Name = collection.Name,
                    Owner = collection.Owner,
                    Description = collection.Description,
                    Theme = collection.Theme,
                    Image = null,
                    ImageName = "Empty.png",
                    ImagePath = path,
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
            string path = "/images/Collections/" + collection.Image.FileName;

            using(var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await collection.Image.CopyToAsync(fileStream);
            }

            var updateCollection = await _context.Collections.FindAsync(collection.CollectionId);
            if (updateCollection != null)
            {
                updateCollection.Name = collection.Name;
                updateCollection.Theme = collection.Theme;
                updateCollection.Image = collection.Image;
                updateCollection.ImageName = collection.Image.FileName;
                updateCollection.ImagePath = path;
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

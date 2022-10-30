using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
                    Tags = _context.Tags.ToList(),
                    Users = new List<ApplicationUser>(),
                };       

                await _context.Collections.AddAsync(newCollection);
                await _context.SaveChangesAsync();


                var tagFromDb = _context.Tags.ToList();

                foreach (var tag in tagFromDb)
				{
                    var collectionFromDb = _context.Collections
                        .Where(c => c.Tags
                        .Contains(tag))
                        .Include(c => c.Tags)
                        .FirstOrDefault();

                    tag.Collections.Add(collectionFromDb);
                    //collectionFromDb.Tags.Add(tag);
                }

                var user = _context.Users
                    .Where(c => c.UserName == collection.Owner)
                    .Include(c => c.Collections)
                    .FirstOrDefault();

                user.Collections.Add(newCollection);
                newCollection.Users.Add(user);

                return newCollection;
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
                    Tags = _context.Tags.ToList(),
                    Users = new List<ApplicationUser>(),
                };
                await _context.Collections.AddAsync(newCollection);
                await _context.SaveChangesAsync();

                var tagFromDb = _context.Tags.ToList();

                foreach (var tag in tagFromDb)
                {
                    var collectionFromDb = _context.Collections
                        .Where(c => c.Tags
                        .Contains(tag))
                        .Include(c => c.Tags)
                        .Include(c => c.Users)
                        .FirstOrDefault();

                    tag.Collections.Add(collectionFromDb);
                    //collectionFromDb.Tags.Add(tag);
                }

                var user = _context.Users
                    .Where(c => c.UserName == collection.Owner)
                    .Include(c => c.Collections)
                    .FirstOrDefault();

                user.Collections.Add(newCollection);
                newCollection.Users.Add(user);

                return newCollection;
            }
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

        public async Task<ApplicationUser> GetAll(string name)
        {
            var user = _context.Users.Where(c => c.UserName == name).FirstOrDefault();
            return user;
        }

        public async Task<List<Collection>> GetTheBiggestCollections()
        {
            var collections = await _context.Collections
                .Include(c => c.Items)
                .OrderByDescending(c => c.Items.Count())
                .Take(6)
                .ToListAsync();
            return collections;
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

        public async Task<List<Collection>> GetCollectionsForParticularUser(ApplicationUser user)
        {
            return await _context.Collections
                .Where(c => c.Users
                .Contains(user))
                .Include(c => c.Users)
                .ToListAsync();
        }

        public async Task<List<Collection>> GetCollectionForParticularItem(Item item)
        {
            return await _context.Collections
                .Where(c => c.Items
                .Contains(item))
                .Include(i => i.Items)
                .Include(i => i.Tags)
                .ToListAsync();
        }

        public async Task<Collection> EditCollection(Collection collection)
        {
            var updateCollection = await _context.Collections.FindAsync(collection.CollectionId);

            if (collection.Image != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(collection.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)collection.Image.Length);
                }
                collection.ImageData = imageData;

                if (updateCollection != null)
                {
                    updateCollection.Name = collection.Name;
                    updateCollection.Theme = collection.Theme;
                    updateCollection.Image = collection.Image;
                    updateCollection.ImageName = collection.Image.FileName;
                    updateCollection.Description = collection.Description;
                    updateCollection.Owner = collection.Owner;
                    updateCollection.ImageData = collection.ImageData;

                    await _context.SaveChangesAsync();

                    return updateCollection;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            else
            {
                string path = "/images/Collections/Empty.png";

                updateCollection.Name = collection.Name;
                updateCollection.Theme = collection.Theme;
                updateCollection.Image = null;
                updateCollection.ImageName = "Empty.png";
                updateCollection.ImageData = null;
                updateCollection.ImagePath = path;
                updateCollection.Owner = collection.Owner;
                updateCollection.Description = collection.Description;

                await _context.SaveChangesAsync();

                return updateCollection;
            }
        }

        public async Task<Collection> AddItemToCollection(Collection collection, Item item, List<string> tags)
        {
            var collectionFromDb = _context.Collections
                .Where(c => c.CollectionId == collection.CollectionId)
                .Include(c => c.Items)
                .Include(c => c.Tags)
                .FirstOrDefault();

            var search = _context.Items
                .Where(s => s.Name == item.Name)
                .Any();

            if (search)
            {
                var itemFromDb = _context.Items
                    .Where(i => i.Name == item.Name)
                    .Include(i => i.Collections)
                    .Include(i => i.Tags)
                    .FirstOrDefault();

                itemFromDb.Collections.Add(collectionFromDb);
                foreach(var tag in tags)
                {   
                    var tagFromDb = _context.Tags
                        .Where(t => t.Name == tag)
                        .Include(t => t.Collections)
                        .Include(t => t.Items)
                        .FirstOrDefault();

                    if (!collectionFromDb.Tags.Contains(tagFromDb))
                    {
                        collectionFromDb.Tags.Add(tagFromDb);
                    }
                    itemFromDb.Tags.Add(tagFromDb);
                }
                collectionFromDb.Items.Add(itemFromDb);
            }
            else
            {
                var newItem = new Item() { ItemId = Guid.NewGuid(), Name = item.Name };
                await _context.Items.AddAsync(newItem);
                await _context.SaveChangesAsync();

                var itemFromDb = _context.Items
                    .Where(i => i.Name == item.Name)
                    .Include(i => i.Collections)
                    .Include(i => i.Tags)
                    .FirstOrDefault();

                foreach (var tag in tags)
                {
                    var tagFromDb = _context.Tags
                        .Where(t => t.Name == tag)
                        .Include(t => t.Collections)
                        .Include(t => t.Items)
                        .FirstOrDefault();

                    if (!collectionFromDb.Tags.Contains(tagFromDb))
                    {
                        collectionFromDb.Tags.Add(tagFromDb);
                    }
                    itemFromDb.Tags.Add(tagFromDb);
                }

                itemFromDb.Collections.Add(collectionFromDb);
                collectionFromDb.Items.Add(itemFromDb);
            }

            await _context.SaveChangesAsync();

            return collectionFromDb;
        }

        public async Task<Collection> RemoveItemFromCollection(Item item)
        {
            var itemFromDb = _context.Items
                .Where(i => i.ItemId == item.ItemId)
                .Include(i => i.Collections)
                .FirstOrDefault();

            var collectionFromDb = _context.Collections
                .Where(c => c.Items
                .Contains(itemFromDb))
                .FirstOrDefault();

            itemFromDb.Collections.Remove(collectionFromDb);
            collectionFromDb.Items.Remove(itemFromDb);

            await _context.SaveChangesAsync();

            return collectionFromDb;
        }
    }
}

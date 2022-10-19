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
    public class TagsRepository : ITagsRepository
    {
        private readonly ApplicationDbContext _context;

        public TagsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> AddTag(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        public void DeleteTagById(Guid id)
        {
            var tag = _context.Tags.Find(id);

            if(tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<List<Tag>> GetAll()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagById(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if(tag != null)
            {
                return tag;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<Tag> EditTag(Tag tag)
        {
            var updateTag = await _context.Tags.FindAsync(tag.TagId);

            if(updateTag != null)
            {
                updateTag.Name = tag.Name;

                _context.SaveChanges();

                return tag;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}

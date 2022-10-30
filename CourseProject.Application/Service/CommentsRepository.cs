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
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetCommentsForParticularItem(Item item)
        {
            return await _context.Comments
                .Where(c => c.Item
                .Equals(item))
                .Include(c => c.User)
                .Include(c => c.Item)
                .ToListAsync();
        }
    }
}

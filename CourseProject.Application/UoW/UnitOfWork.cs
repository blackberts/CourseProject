using CourseProject.Application.Service;
using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private IItemsRepository _itemsRepository;
        private ITagsRepository _tagsRepository;
        private IUsersRepository _usersRepository;
        private IAccountRepository _accountRepository;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IAccountRepository Account
        {
            get
            {
                return _accountRepository = _accountRepository ?? new AccountRepository(_userManager, _signInManager, _roleManager, _context);
            }
        }

        public IUsersRepository Users
        {
            get
            {
                return _usersRepository = _usersRepository ?? new UsersRepository(_userManager, _context);
            }
        }

        public IItemsRepository Items
        {
            get
            {
                return _itemsRepository = _itemsRepository ?? new ItemsRepository(_context);
            }
        }
        public ITagsRepository Tags
        {
            get
            {
                return _tagsRepository = _tagsRepository ?? new TagsRepository(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Service
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UsersRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<AuthResult> DeleteUser(string userToDelete)
        {
            var user = await _userManager.FindByIdAsync(userToDelete);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new AuthResult()
            {
                Result = true
            };
        }

        public async Task<AuthResult> BanUnbanUser(string userToBanUnban)
        {
            var user = await _userManager.FindByIdAsync(userToBanUnban);

            if (user.Status == "Unactive")
            {
                user.Status = "Banned";
                await _userManager.UpdateAsync(user);
            }
            else if (user.Status == "Active")
            {
                user.Status = "Banned";
                await _userManager.UpdateAsync(user);
            }
            else if (user.Status == "Banned")
            {
                user.Status = "Unactive";
                await _userManager.UpdateAsync(user);
            }                
            
            await _context.SaveChangesAsync();

            return new AuthResult()
            {
                Result = true
            };
        }

        public async Task<AuthResult> ChangeRoleUser(string userToChangeRole)
        {
            var user = await _userManager.FindByIdAsync(userToChangeRole);

            if(user.Role == "User")
            {
                user.Role = "Admin";
                await _userManager.RemoveFromRoleAsync(user, "User");
                await _userManager.AddToRoleAsync(user, "Admin");
                await _userManager.UpdateAsync(user);

                await _context.SaveChangesAsync();

                return new AuthResult()
                {
                    Result = true
                };
            }
            if (user.Role == "Admin")
            {
                user.Role = "User";
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "User");
                await _userManager.UpdateAsync(user);

                await _context.SaveChangesAsync();

                return new AuthResult()
                {
                    Result = true
                };
            }

            return new AuthResult()
            {
                Result = false,
                Error = "Something wrong in change role"
            };
        }
    }
}

using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Entities.DTOs;
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
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<AuthResult> Login(AccountLoginDto loginRequest)
        {
            var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (existingUser is null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Error = "Wrong credentials. Please, try again!"
                };
            }
             
            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);
            if (!isCorrect)
            {
                return new AuthResult()
                {
                    Result = false,
                    Error = "Wrong credentials. Please, try again!"
                };
            }

            if (existingUser.Status == "Active")
            {
                existingUser.Status = "Unactive";

                await _userManager.UpdateAsync(existingUser);
                return new AuthResult()
                {
                    Result = false,
                    Error = "You are already active, please login one more time"
                };
            }

            if (existingUser.Status == "Banned")
            {
                return new AuthResult()
                {
                    Result = false,
                    Error = "You are banned"
                };
            }

            var result = await _signInManager.PasswordSignInAsync(existingUser, loginRequest.Password, false, false);
            if (result.Succeeded)
            {
                if (existingUser.Status == "Unactive")
                {
                    existingUser.Last_login = DateTime.Now;
                    existingUser.Status = "Active";

                    await _userManager.UpdateAsync(existingUser);
                    return new AuthResult()
                    {
                        Result = true,
                        UserId = existingUser.Id.ToString(),
                        UserName = existingUser.UserName,
                    };
                }
            }
            return new AuthResult()
            {
                Result = false,
                Error = "Wrong credentials. Please, try again!"
            };
        }

        public async Task<AuthResult> LogOut()
        {
            var activeUser = await _context.Users.Where(x => x.Status == "Active").ToListAsync();

            foreach (var user in activeUser)
            {
                user.Status = "Unactive";
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.SignOutAsync();
            return new AuthResult()
            {
                Result = true
            };
        }

        //public Task<AuthResult> Profile()
        //{
            
        //}

        public async Task<AuthResult> Register(AccountRegisterDto registerRequest)
        {
            // add roles
            if (!(await _roleManager.RoleExistsAsync("User")) && !(await _roleManager.RoleExistsAsync("Admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
            if(existingUser != null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Error = "This Email address is already in use"
                };
            }

            var newUser = new ApplicationUser()
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                Created_At = DateTime.UtcNow,
                Status = "Unactive",
                Role = "User"
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerRequest.Password);

            var userFromDb = await _userManager.FindByNameAsync(newUser.UserName);
            await _userManager.AddToRoleAsync(userFromDb, "User");

            if (newUserResponse.Succeeded)
            {
                return new AuthResult()
                {
                    Result = true
                };
            }

            return new AuthResult()
            {
                Result = false,
                Error = "Something wrong"
            };
        }

        public async Task<AuthResult> ChangeTheme(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
 
            if(user.Theme == "White")
            {
                user.Theme = "Dark";
                await _userManager.UpdateAsync(user);
            }
            else if(user.Theme == "Dark")
            {
                user.Theme = "White";
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.Theme = "White";
                await _userManager.UpdateAsync(user);
            }

            return new AuthResult()
            {
                Result = true,
                UserId = user.Id
            };
        }
    }
}

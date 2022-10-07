using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Entities.DTOs;
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
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<AuthResult> Login(UserLoginDto loginRequest)
        {
            var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (existingUser != null)
            {
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);
                if (isCorrect)
                {
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
                                Result = true
                            };
                        }
                        if(existingUser.Status == "Banned")
                        {
                            return new AuthResult()
                            {
                                Result = false,
                                Error = "You are banned"
                            };
                        }
                    }
                    return new AuthResult()
                    {
                        Result = false,
                        Error = "Wrong credentials. Please, try again!"
                    };
                }
                return new AuthResult()
                {
                    Result = false,
                    Error = "Wrong credentials. Please, try again!"
                };
            }
            return new AuthResult()
            {
                Result = false,
                Error = "Wrong credentials. Please, try again!"
            };
        }

        public async void LogOut()
        {
            var activeUser = await _context.Users.Where(x => x.Status == "Active").ToListAsync(); // error

            foreach (var user in activeUser)
            {
                user.Status = "Unactive";
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.SignOutAsync();
        }

        public async Task<AuthResult> Register(UserRegisterDto registerRequest)
        {
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
                Created_At = DateTime.Now,
                Status = "Unactive"
            };
            await _userManager.CreateAsync(newUser, registerRequest.Password);

            return new AuthResult()
            {
                Result = true
            };
        }
    }
}

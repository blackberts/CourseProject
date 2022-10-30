using CourseProject.Domain.Entities;
using CourseProject.Domain.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataContext.Repositories
{
    public interface IAccountRepository
    {
        Task<AuthResult> Register(AccountRegisterDto registerRequest);
        Task<AuthResult> Login(AccountLoginDto loginRequest);
        Task<AuthResult> ChangeTheme(string name);
        Task<AuthResult> LogOut();
        //Task<AuthResult> Profile();
    }
}

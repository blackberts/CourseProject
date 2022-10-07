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
    public interface IAuthRepository
    {
        Task<AuthResult> Register(UserRegisterDto registerRequest);
        Task<AuthResult> Login(UserLoginDto loginRequest);
        void LogOut();
    }
}

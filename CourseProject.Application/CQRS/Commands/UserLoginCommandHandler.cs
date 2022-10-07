using AutoMapper;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Entities.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, AuthResult>
    {
        private readonly IAuthRepository _IAuth;
        private readonly IMapper _Mapper;

        public UserLoginCommandHandler(IAuthRepository iAuth, IMapper mapper)
        {
            _IAuth = iAuth;
            _Mapper = mapper;
        }

        public async Task<AuthResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = _Mapper.Map<UserLoginDto>(request);
            var loginUser = await _IAuth.Login(user);
            return loginUser;
        }
    }
}

using AutoMapper;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Entities.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, AuthResult>
    {
        private readonly IAuthRepository _IAuth;
        private readonly IMapper _Mapper;

        public UserRegisterCommandHandler(IAuthRepository iAuth, IMapper mapper)
        {
            _IAuth = iAuth;
            _Mapper = mapper;
        }
        public async Task<AuthResult> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _Mapper.Map<UserRegisterDto>(request);
            var createUser = await _IAuth.Register(user);
            return createUser;
        }
    }
}

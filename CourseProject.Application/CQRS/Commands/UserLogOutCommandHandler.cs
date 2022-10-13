using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands
{
    public class UserLogOutCommandHandler : IRequestHandler<UserLogOutCommand, AuthResult>
    {
        private readonly IAuthRepository _IAuth;

        public UserLogOutCommandHandler(IAuthRepository iAuth)
        {
            _IAuth = iAuth;
        }

        public async Task<AuthResult> Handle(UserLogOutCommand request, CancellationToken cancellationToken)
        {
            var logoutUser = await _IAuth.LogOut();
            return logoutUser;
        }
    }
}

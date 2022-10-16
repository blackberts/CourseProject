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
    public class UserChangeRoleCommandHandler : IRequestHandler<UserChangeRoleCommand, AuthResult>
    {
        private readonly IUsersRepository _IUsers;

        public UserChangeRoleCommandHandler(IUsersRepository iUsers)
        {
            _IUsers = iUsers;
        }

        public async Task<AuthResult> Handle(UserChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _IUsers.ChangeRoleUser(request.Id);
            return result;
        }
    }
}

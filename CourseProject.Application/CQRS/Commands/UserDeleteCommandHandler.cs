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
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, AuthResult>
    {
        private readonly IUsersRepository _IUsers;

        public UserDeleteCommandHandler(IUsersRepository iUsers)
        {
            _IUsers = iUsers;
        }

        public async Task<AuthResult> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await _IUsers.DeleteUser(request.Id);
            return result;
        }
    }
}

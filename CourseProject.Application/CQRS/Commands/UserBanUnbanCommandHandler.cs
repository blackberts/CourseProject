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
    public class UserBanUnbanCommandHandler : IRequestHandler<UserBanUnbanCommand, AuthResult>
    {
        private readonly IUsersRepository _IUsers;

        public UserBanUnbanCommandHandler(IUsersRepository iUsers)
        {
            _IUsers = iUsers;
        }

        public async Task<AuthResult> Handle(UserBanUnbanCommand request, CancellationToken cancellationToken)
        {
            var result = await _IUsers.BanUnbanUser(request.Id);
            return result;
        }
    }
}

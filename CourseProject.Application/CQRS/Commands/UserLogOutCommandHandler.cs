using CourseProject.DataContext.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands
{
    public class UserLogOutCommandHandler : IRequestHandler<UserLogOutCommand>
    {
        private readonly IAuthRepository _IAuth;

        public UserLogOutCommandHandler(IAuthRepository iAuth)
        {
            _IAuth = iAuth;
        }

        public Task<Unit> Handle(UserLogOutCommand request, CancellationToken cancellationToken)
        {
            _IAuth.LogOut();
            return Task.FromResult(Unit.Value);
        }
    }
}

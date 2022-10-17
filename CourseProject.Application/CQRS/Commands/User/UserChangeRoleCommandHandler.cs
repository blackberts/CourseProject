using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.User
{
    public class UserChangeRoleCommandHandler : IRequestHandler<UserChangeRoleCommand, AuthResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserChangeRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Handle(UserChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Users.ChangeRoleUser(request.Id);
            return result;
        }
    }
}

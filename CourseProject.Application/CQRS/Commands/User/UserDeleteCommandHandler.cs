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
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, AuthResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Users.DeleteUser(request.Id);
            return result;
        }
    }
}

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
    public class UserUnbanCommandHandler : IRequestHandler<UserUnbanCommand, AuthResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserUnbanCommandHandler( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Handle(UserUnbanCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Users.UnbanUser(request.Id);
            return result;
        }
    }
}

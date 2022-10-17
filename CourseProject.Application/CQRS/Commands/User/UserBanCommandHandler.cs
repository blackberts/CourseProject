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
    public class UserBanCommandHandler : IRequestHandler<UserBanCommand, AuthResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserBanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Handle(UserBanCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Users.BanUser(request.Id);
            return result;
        }
    }
}

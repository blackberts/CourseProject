using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Account
{
    public class AccountLogOutCommandHandler : IRequestHandler<AccountLogOutCommand, AuthResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountLogOutCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Handle(AccountLogOutCommand request, CancellationToken cancellationToken)
        {
            var logoutUser = await _unitOfWork.Account.LogOut();
            return logoutUser;
        }
    }
}

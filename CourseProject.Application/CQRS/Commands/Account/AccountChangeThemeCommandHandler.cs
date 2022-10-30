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
    public class AccountChangeThemeCommandHandler : IRequestHandler<AccountChangeThemeCommand, AuthResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountChangeThemeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Handle(AccountChangeThemeCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Account.ChangeTheme(request.Name);
            _unitOfWork.Save();
            return user;
        }
    }
}

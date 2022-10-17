using AutoMapper;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Entities.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Account
{
    public class AccountRegisterCommandHandler : IRequestHandler<AccountRegisterCommand, AuthResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public AccountRegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        public async Task<AuthResult> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _Mapper.Map<AccountRegisterDto>(request);
            var createUser = await _unitOfWork.Account.Register(user);
            return createUser;
        }
    }
}

using AutoMapper;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Entities.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Account
{
    public class AccountLoginCommandHandler : IRequestHandler<AccountLoginCommand, AuthResult>
    {
        private readonly IMapper _Mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountLoginCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _Mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
        {
            var user = _Mapper.Map<AccountLoginDto>(request);
            var loginUser = await _unitOfWork.Account.Login(user);
            return loginUser;
        }
    }
}

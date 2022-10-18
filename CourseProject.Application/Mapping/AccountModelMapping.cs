using AutoMapper;
using CourseProject.Application.CQRS.Commands.Account;
using CourseProject.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Mapping
{
    public class AccountModelMapping : Profile
    {
        public AccountModelMapping()
        {
            CreateMap<AccountLoginCommand, AccountLoginDto>().ReverseMap();
            CreateMap<AccountRegisterCommand, AccountRegisterDto>().ReverseMap();
        }
    }
}

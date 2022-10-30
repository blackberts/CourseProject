using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Account
{
    public class AccountChangeThemeCommand : IRequest<AuthResult>
    {
        public string? Name { get; set; }
    }
}

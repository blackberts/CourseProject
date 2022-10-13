using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands
{
    public class UserDeleteCommand : IRequest<AuthResult>
    {
        public string? Id { get; set; }
    }
}

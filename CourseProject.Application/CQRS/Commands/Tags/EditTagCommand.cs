using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Tags
{
    public class EditTagCommand : IRequest<Tag>
    {
        public Guid TagId { get; set; }
        public string? Name { get; set; }
    }
}

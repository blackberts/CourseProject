using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Items
{
    public class DeleteItemCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

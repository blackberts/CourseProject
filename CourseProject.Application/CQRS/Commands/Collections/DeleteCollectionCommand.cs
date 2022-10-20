using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Collections
{
    public class DeleteCollectionCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

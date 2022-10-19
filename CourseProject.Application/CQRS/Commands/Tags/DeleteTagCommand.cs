using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Tags
{
    public class DeleteTagCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

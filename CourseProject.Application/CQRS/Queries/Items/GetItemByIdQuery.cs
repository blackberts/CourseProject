using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Queries.Items
{
    public class GetItemByIdQuery : IRequest<Item>
    {
        public Guid Id { get; set; }

        public GetItemByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

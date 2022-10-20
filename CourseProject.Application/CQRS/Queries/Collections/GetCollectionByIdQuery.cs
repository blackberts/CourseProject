using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Queries.Collections
{
    public class GetCollectionByIdQuery : IRequest<Collection>
    {
        public Guid Id { get; set; }

        public GetCollectionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

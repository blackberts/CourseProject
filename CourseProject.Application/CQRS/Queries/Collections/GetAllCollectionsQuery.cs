using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Queries.Collections
{
    public class GetAllCollectionsQuery : IRequest<ApplicationUser>
    {
        public string? Name { get;set; }
    }
}

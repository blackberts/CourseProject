using CourseProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Collections
{
    public class CreateCollectionCommand : IRequest<Collection>
    {
        public string? Name { get; set; }
        public string? Owner { get; set; } = Environment.UserName;
        public string? Description { get; set; }
        public string? Theme { get; set; }
        public IFormFile? Image { get; set; }
    }
}

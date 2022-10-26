using CourseProject.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Collections
{
    public class AddItemToCollectionCommand : IRequest<Collection>
    {
        public Guid CollectionId { get; set; }
        public string? Name { get; set; }
        public List<string>? Tags { get; set; }
    }
}

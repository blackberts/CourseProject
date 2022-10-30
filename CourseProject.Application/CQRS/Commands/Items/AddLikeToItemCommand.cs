using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Items
{
    public class AddLikeToItemCommand : IRequest<Item>
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
    }
}

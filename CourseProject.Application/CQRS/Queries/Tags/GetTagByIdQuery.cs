﻿using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Queries.Tags
{
    public class GetTagByIdQuery : IRequest<Tag>
    {
        public Guid Id { get; set; }

        public GetTagByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

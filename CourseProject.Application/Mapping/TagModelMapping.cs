using AutoMapper;
using CourseProject.Application.CQRS.Commands.Tags;
using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Mapping
{
    public class TagModelMapping : Profile
    {
        public TagModelMapping()
        {
            CreateMap<CreateTagCommand, Tag>().ReverseMap();
            CreateMap<EditTagCommand, Tag>().ReverseMap();
        }
    }
}

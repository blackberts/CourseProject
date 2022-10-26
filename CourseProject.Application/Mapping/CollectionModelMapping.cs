using AutoMapper;
using CourseProject.Application.CQRS.Commands.Collections;
using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Mapping
{
    public class CollectionModelMapping : Profile
    {
        public CollectionModelMapping()
        {
            CreateMap<CreateCollectionCommand, Collection>().ReverseMap();
            CreateMap<EditCollectionCommand, Collection>().ReverseMap();
            CreateMap<AddItemToCollectionCommand, Collection>().ReverseMap();
        }
    }
}

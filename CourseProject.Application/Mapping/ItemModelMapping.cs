using AutoMapper;
using CourseProject.Application.CQRS.Commands.Collections;
using CourseProject.Application.CQRS.Commands.Items;
using CourseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Mapping
{
    public class ItemModelMapping : Profile
    {
        public ItemModelMapping()
        {
            CreateMap<CreateItemCommand, Item>().ReverseMap();
            CreateMap<EditItemCommand, Item>().ReverseMap();
            CreateMap<AddItemToCollectionCommand, Item>().ReverseMap();
            CreateMap<RemoveItemFromCollectionCommand, Item>().ReverseMap();
        }
    }
}

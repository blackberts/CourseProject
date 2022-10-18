using AutoMapper;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Items
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Item>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public UpdateItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;
        }

        public async Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _Mapper.Map<Item>(request);
            var updateItem = await _unitOfWork.Items.UpdateItem(item);
            _unitOfWork.Save();
            return updateItem;
        }
    }
}

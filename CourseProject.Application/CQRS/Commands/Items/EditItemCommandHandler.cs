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
    public class EditItemCommandHandler : IRequestHandler<EditItemCommand, Item>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public EditItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;
        }

        public async Task<Item> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            var item = _Mapper.Map<Item>(request);
            var updateItem = await _unitOfWork.Items.EditItem(item);
            _unitOfWork.Save();
            return updateItem;
        }
    }
}

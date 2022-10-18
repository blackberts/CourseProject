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
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Item>
    {
        private readonly IMapper _Mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateItemCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _Mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Item> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _Mapper.Map<Item>(request);
            var addItem = await _unitOfWork.Items.AddItem(item);
            _unitOfWork.Save();
            return addItem;
        }
    }
}

using AutoMapper;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Collections
{
    public class RemoveItemFromCollectionCommandHandler : IRequestHandler<RemoveItemFromCollectionCommand, Collection>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper Mapper;

        public RemoveItemFromCollectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<Collection> Handle(RemoveItemFromCollectionCommand request, CancellationToken cancellationToken)
        {
            var item = Mapper.Map<Item>(request);
            var removeItemFromCollection = await _unitOfWork.Collections.RemoveItemFromCollection(item);
            _unitOfWork.Save();
            return removeItemFromCollection;
        }
    }
}

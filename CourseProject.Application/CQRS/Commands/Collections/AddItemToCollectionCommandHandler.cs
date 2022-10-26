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
    public class AddItemToCollectionCommandHandler : IRequestHandler<AddItemToCollectionCommand, Collection>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper Mapper;

        public AddItemToCollectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<Collection> Handle(AddItemToCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = new Collection() { CollectionId = request.CollectionId };
            var item = new Item() { Name = request.Name };
            var tags = new List<string>(request.Tags);
            var addItemToCollection = await _unitOfWork.Collections.AddItemToCollection(collection, item, tags);
            _unitOfWork.Save();
            return addItemToCollection;
        }
    }
}

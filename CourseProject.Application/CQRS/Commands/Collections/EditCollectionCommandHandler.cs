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
    public class EditCollectionCommandHandler : IRequestHandler<EditCollectionCommand, Collection>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper Mapper;

        public EditCollectionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<Collection> Handle(EditCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = Mapper.Map<Collection>(request);
            var addCollection = await _unitOfWork.Collections.EditCollection(collection);
            _unitOfWork.Save();
            return addCollection;
        }
    }
}

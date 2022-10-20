using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Queries.Collections
{
    public class GetCollectionByIdQueryHandler : IRequestHandler<GetCollectionByIdQuery, Collection>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCollectionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Collection> Handle(GetCollectionByIdQuery request, CancellationToken cancellationToken)
        {
            var collection = await _unitOfWork.Collections.GetCollectionById(request.Id);
            _unitOfWork.Save();
            return collection;
        }
    }
}

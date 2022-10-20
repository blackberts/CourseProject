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
    public class GetAllCollectionsQueryHandler : IRequestHandler<GetAllCollectionsQuery, List<Collection>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCollectionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Collection>> Handle(GetAllCollectionsQuery request, CancellationToken cancellationToken)
        {
            var collections = await _unitOfWork.Collections.GetAll();
            _unitOfWork.Save();
            return collections;
        }
    }
}

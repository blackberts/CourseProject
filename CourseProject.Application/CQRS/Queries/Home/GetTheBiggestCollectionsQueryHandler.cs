using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Queries.Home
{
    public class GetTheBiggestCollectionsQueryHandler : IRequestHandler<GetTheBiggestCollectionsQuery, List<Collection>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTheBiggestCollectionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Collection>> Handle(GetTheBiggestCollectionsQuery request, CancellationToken cancellationToken)
        {
            var collections = await _unitOfWork.Collections.GetTheBiggestCollections();
            _unitOfWork.Save();
            return collections;
        }
    }
}

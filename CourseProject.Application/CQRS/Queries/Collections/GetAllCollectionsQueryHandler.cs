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
    public class GetAllCollectionsQueryHandler : IRequestHandler<GetAllCollectionsQuery, ApplicationUser>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCollectionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> Handle(GetAllCollectionsQuery request, CancellationToken cancellationToken)
        {
            var collections = await _unitOfWork.Collections.GetAll(request.Name);
            _unitOfWork.Save();
            return collections;
        }
    }
}

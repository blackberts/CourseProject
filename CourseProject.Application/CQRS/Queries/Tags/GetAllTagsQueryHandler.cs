using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Queries.Tags
{
    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<Tag>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTagsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Tag>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _unitOfWork.Tags.GetAll();
            _unitOfWork.Save();
            return tags;
        }
    }
}

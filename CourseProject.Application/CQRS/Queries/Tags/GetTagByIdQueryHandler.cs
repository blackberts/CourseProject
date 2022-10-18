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
    public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, Tag>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTagByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tag> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _unitOfWork.Tags.GetTagById(request.Id);
            _unitOfWork.Save();
            return tag;
        }
    }
}

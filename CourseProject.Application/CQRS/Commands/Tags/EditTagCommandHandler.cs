using AutoMapper;
using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Tags
{
    public class EditTagCommandHandler : IRequestHandler<EditTagCommand, Tag>
    {
        private readonly IMapper _Mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditTagCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _Mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tag> Handle(EditTagCommand request, CancellationToken cancellationToken)
        {
            var tag = _Mapper.Map<Tag>(request);
            var updateTag = await _unitOfWork.Tags.EditTag(tag);
            return updateTag;
        }
    }
}

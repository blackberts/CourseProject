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
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Tag>
    {
        private readonly IMapper _Mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTagCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _Mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = _Mapper.Map<Tag>(request);
            var addTag = await _unitOfWork.Tags.AddTag(tag);
            return addTag;
        }
    }
}

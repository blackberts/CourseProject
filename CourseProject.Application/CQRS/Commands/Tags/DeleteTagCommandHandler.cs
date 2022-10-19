using CourseProject.DataContext.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Tags
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTagCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Tags.DeleteTagById(request.Id);
            _unitOfWork.Save();
            return Task.FromResult(Unit.Value);
        }
    }
}

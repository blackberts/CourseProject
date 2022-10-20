using CourseProject.DataContext.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Collections
{
    public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCollectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Collections.DeleteCollectionById(request.Id);
            _unitOfWork.Save();
            return Task.FromResult(Unit.Value);
        }
    }
}

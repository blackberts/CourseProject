using CourseProject.DataContext.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Items
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.Items.DeleteItemById(request.Id);
            _unitOfWork.Save();
            return Task.FromResult(Unit.Value);
        }
    }
}

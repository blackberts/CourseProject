using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Commands.Items
{
    public class DeleteCommentFromItemCommandHandler : IRequestHandler<DeleteCommentFromItemCommand, Item>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommentFromItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Item> Handle(DeleteCommentFromItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.Items.DeleteComment(request.Id);
            _unitOfWork.Save();
            return item;
        }
    }
}

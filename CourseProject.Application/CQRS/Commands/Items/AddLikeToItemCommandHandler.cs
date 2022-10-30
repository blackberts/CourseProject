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
    public class AddLikeToItemCommandHandler : IRequestHandler<AddLikeToItemCommand, Item>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddLikeToItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Item> Handle(AddLikeToItemCommand request, CancellationToken cancellationToken)
        {
            var itemWithLikes = await _unitOfWork.Items.AddLike(request.ItemId, request.UserId);
            _unitOfWork.Save();
            return itemWithLikes;
        }
    }
}

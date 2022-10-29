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
    public class AddCommentToItemCommandHandler : IRequestHandler<AddCommentToItemCommand, Item>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCommentToItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Item> Handle(AddCommentToItemCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment()
            {
                CommentId = Guid.NewGuid(),
                Text = request.Name,
                CreatedDate = DateTime.Now
            };
            var user = new ApplicationUser() { Id = request.UserId.ToString() };
            var item = new Item() { ItemId = request.ItemId };
            var itemWithComment = await _unitOfWork.Items.AddComment(user, item, comment);
            _unitOfWork.Save();

            return itemWithComment;
        }
    }
}

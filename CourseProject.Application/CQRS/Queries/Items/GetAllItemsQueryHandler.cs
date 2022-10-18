using CourseProject.DataContext.Repositories;
using CourseProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.CQRS.Queries.Items
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, List<Item>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllItemsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Item>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.Items.GetAll();
            _unitOfWork.Save();
            return items;
        }
    }
}

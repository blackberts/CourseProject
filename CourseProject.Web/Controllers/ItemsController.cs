using CourseProject.Application.CQRS.Commands.Items;
using CourseProject.Application.CQRS.Queries.Items;
using CourseProject.DataContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IMediator Mediator;
        private readonly ApplicationDbContext _context;

        public ItemsController(IMediator mediator, ApplicationDbContext context)
        {
            Mediator = mediator;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Items.ToListAsync();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetItemByIdQuery(id);
            var result = await Mediator.Send(query);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllItemsQuery();
            var result = await Mediator.Send(query);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Items");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateItemCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Items");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteItemCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Items");
        }
    }
}

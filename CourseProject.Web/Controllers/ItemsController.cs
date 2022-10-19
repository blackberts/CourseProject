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

        // GET
        public async Task<IActionResult> Index()
        {
            var query = new GetAllItemsQuery();
            var result = await Mediator.Send(query);
            return View(result);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetItemByIdQuery(id);
            var result = await Mediator.Send(query);
            return View(result);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Items");
        }

        // GET
        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var itemFromDb = _context.Items.Find(id);

            if (itemFromDb == null)
            {
                return NotFound();
            }

            return View(itemFromDb);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Edit(EditItemCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Items");
        }


        // GET
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var itemFromDb = _context.Items.Find(id);

            if (itemFromDb == null)
            {
                return NotFound();
            }

            return View(itemFromDb);
        }


        // POST
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteItemCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Items");
        }
    }
}

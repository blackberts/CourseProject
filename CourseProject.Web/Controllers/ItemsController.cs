using CourseProject.Application.CQRS.Commands.Collections;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = new GetAllItemsQuery();
            var result = await Mediator.Send(query);
            return View(result);
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetItemByIdQuery(id);
            var result = await Mediator.Send(query);
            return View(result);
        }

        [HttpGet]
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

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var itemFromDb = _context.Items.Find(id);
            var collectionFromDb = _context.Collections
                .Where(c => c.Items
                .Contains(itemFromDb))
                .FirstOrDefault();
            TempData["collection"] = collectionFromDb.CollectionId;

            if (itemFromDb == null)
            {
                return NotFound();
            }

            return View(itemFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditItemCommand command)
        {
            var item = await Mediator.Send(command);
            
            var collection = _context.Collections
                .Where(c => c.Items
                .Contains(item))
                .FirstOrDefault();
            TempData["collection"] = collection.CollectionId;

            return RedirectToAction("Get", "Collections");
        }


        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteItemCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Items");
        }
    }
}

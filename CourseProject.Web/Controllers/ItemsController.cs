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
        public async Task<IActionResult> Index(GetAllItemsQuery query)
        {
            var result = await Mediator.Send(query);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetItemByIdQuery query)
        {
            if(query.Id == Guid.Empty)
            {
                query.Id = (Guid)TempData["itemId"];
                var item = await Mediator.Send(query);
                TempData["itemId"] = item.ItemId;
                return View(item);
            }
            else
            {
                var item = await Mediator.Send(query);
                TempData["itemId"] = item.ItemId;
                return View(item);
            }
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

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentToItemCommand command)
        {
            command.ItemId = (Guid)TempData["itemId"];
            command.UserId = Request.Cookies["userId"];
            await Mediator.Send(command);
            return RedirectToAction("Get", "Items");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            DeleteCommentFromItemCommand command = new() { Id = id };
            var item = await Mediator.Send(command);
            TempData["itemId"] = item.ItemId;
            return RedirectToAction("Get", "Items");
        }

        public HttpRequest GetRequest()
        {
            return Request;
        }

        [HttpPost]
        public async Task<IActionResult> AddLike(AddLikeToItemCommand command, HttpRequest request)
        {
            command.ItemId = (Guid)TempData["itemId"];
            command.UserId = Request.Cookies["userId"];
            var item = await Mediator.Send(command);
            return RedirectToAction("Get", "Items");
        }
    }
}

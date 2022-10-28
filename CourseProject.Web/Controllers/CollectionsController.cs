using CourseProject.Application.CQRS.Commands.Collections;
using CourseProject.Application.CQRS.Queries.Collections;
using CourseProject.DataContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Web.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator Mediator;

        public CollectionsController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            Mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(GetAllCollectionsQuery query)
        {
            if(query.Name == null)
            {
                GetAllCollectionsQuery newQuery = new() { Name = (string)TempData["user"] };
                var resultQuery = await Mediator.Send(newQuery);

                return View(resultQuery);
            }

            var result = await Mediator.Send(query);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetCollectionByIdQuery query)
        {
            if(query.Id == Guid.Empty)
            {
                GetCollectionByIdQuery newQuery = new() { Id = (Guid)TempData["collection"] };
                var resultQuery = await Mediator.Send(newQuery);

                var user1 = _context.Users
                .Where(c => c.Collections
                .Contains(resultQuery))
                .FirstOrDefault();
                TempData["user"] = user1.UserName;

                return View(resultQuery);
            }

            var result = await Mediator.Send(query);

            var user = _context.Users
                .Where(c => c.Collections
                .Contains(result))
                .FirstOrDefault();
            TempData["user"] = user.UserName;
            return View(result);
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var user = _context.Users.Find(id);
            TempData["owner"] = user.UserName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCollectionCommand command)
        {
            CreateCollectionCommand updateCommand = new()
            {
                Owner = (string)TempData["owner"],
                Description = command.Description,
                Theme = command.Theme,
                Image = command.Image,
                Name = command.Name
            };

            var collection = await Mediator.Send(updateCommand);
            var user = _context.Users
                .Where(c => c.Collections
                .Contains(collection))
                .FirstOrDefault();
            TempData["user"] = user.UserName;
            return RedirectToAction("Index", "Collections");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var collectionFromDb = _context.Collections.Find(id);
            var user = _context.Users
                .Where(c => c.Collections
                .Contains(collectionFromDb))
                .FirstOrDefault();
            TempData["user"] = user.UserName;
            TempData["collection"] = collectionFromDb.CollectionId;

            if (collectionFromDb == null)
            {
                return NotFound();
            }

            return View(collectionFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCollectionCommand command)
        {
            var collection = await Mediator.Send(command);
            var user = _context.Users
                .Where(c => c.Collections
                .Contains(collection))
                .FirstOrDefault();
            TempData["user"] = user.UserName;
            TempData["collection"] = collection.CollectionId;
            return RedirectToAction("Index", "Collections");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var collectionFromDb = _context.Collections.Find(id);
            var user = _context.Users
                .Where(c => c.Collections
                .Contains(collectionFromDb))
                .FirstOrDefault();
            if(user == null)
            {
                TempData["user"] = collectionFromDb.Owner;
            }
            else
            {
                TempData["user"] = user.UserName;
            }
            TempData["collection"] = collectionFromDb.CollectionId;

            if (collectionFromDb == null)
            {
                return NotFound();
            }

            return View(collectionFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCollectionCommand command)
        {
            var collection = await _context.Collections.FindAsync(command.Id);
            var user = _context.Users
                .Where(c => c.Collections
                .Contains(collection))
                .FirstOrDefault();
            if(user == null)
            {
                TempData["user"] = collection.Owner;
            }
            else
            {
                TempData["user"] = user.UserName;
            }

            await Mediator.Send(command);
            return RedirectToAction("Index", "Collections");
        }

        [HttpGet]
        public IActionResult AddItem(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var collectionFromDb = _context.Collections.Find(id);
            TempData["collection"] = collectionFromDb.CollectionId;

            if (collectionFromDb == null)
            {
                return NotFound();
            }

            return View(collectionFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemToCollectionCommand command)
        {
            var collection = await Mediator.Send(command);
            TempData["collection"] = collection.CollectionId;
            return RedirectToAction("Get", "Collections");
        }

        [HttpGet]
        public IActionResult RemoveItem(Guid id)
        {
            if(id == Guid.Empty)
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
        public async Task<IActionResult> RemoveItem(RemoveItemFromCollectionCommand command)
        {
            var collection = await Mediator.Send(command);
            TempData["collection"] = collection.CollectionId;
            return RedirectToAction("Get", "Collections");
        }
    }
}

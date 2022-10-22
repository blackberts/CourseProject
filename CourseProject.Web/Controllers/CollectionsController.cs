using CourseProject.Application.CQRS.Commands.Collections;
using CourseProject.Application.CQRS.Queries.Collections;
using CourseProject.DataContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            var query = new GetAllCollectionsQuery();
            var result = await Mediator.Send(query);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCollectionCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Collections");
        }

        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var collectionFromDb = _context.Collections.Find(id);

            if (collectionFromDb == null)
            {
                return NotFound();
            }

            return View(collectionFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCollectionCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Collections");
        }

        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var collectionFromDb = _context.Collections.Find(id);

            if (collectionFromDb == null)
            {
                return NotFound();
            }

            return View(collectionFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCollectionCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Collections");
        }
    }
}

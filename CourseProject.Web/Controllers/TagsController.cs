using CourseProject.Application.CQRS.Commands.Tags;
using CourseProject.Application.CQRS.Queries.Tags;
using CourseProject.DataContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Web.Controllers
{
    public class TagsController : Controller
    {
        private readonly IMediator Mediator;
        private readonly ApplicationDbContext _context;

        public TagsController(IMediator mediator, ApplicationDbContext context)
        {
            Mediator = mediator;
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var query = new GetAllTagsQuery();
            var result = await Mediator.Send(query);
            return View(result);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetTagByIdQuery(id);
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
        public async Task<IActionResult> Create(CreateTagCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Tags");
        }

        // GET
        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var tagFromDb = _context.Tags.Find(id);

            if (tagFromDb == null)
            {
                return NotFound();
            }

            return View(tagFromDb);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Tags");
        }


        // GET
       public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var tagFromDb = _context.Tags.Find(id);

            if(tagFromDb == null)
            {
                return NotFound();
            }

            return View(tagFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTagCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Tags");
        }
    }
}

using CourseProject.Application.CQRS.Commands;
using CourseProject.DataContext;
using CourseProject.DataContext.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator Mediator;

        public UsersController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            Mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            UserDeleteCommand command = new() { Id = userId };
            await Mediator.Send(command);
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> BanUnban(string userId)
        {
            UserBanUnbanCommand command = new() { Id = userId }; // i found only 1 bastard way to transmit id to command
            await Mediator.Send(command);
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId)
        {
            UserChangeRoleCommand command = new() { Id = userId };
            await Mediator.Send(command);
            return RedirectToAction("Index", "Users");
        }
    }
}

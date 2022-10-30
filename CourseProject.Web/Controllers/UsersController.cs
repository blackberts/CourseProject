using CourseProject.Application.CQRS.Commands.User;
using CourseProject.Application.CQRS.Queries.Users;
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

        [HttpGet]
        public async Task<IActionResult> Index(GetAllUsersQuery query)
        {
            var users = await Mediator.Send(query);
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
        public async Task<IActionResult> Ban(string userId)
        {
            UserBanCommand command = new() { Id = userId }; // i found only 1 bastard way to transmit id to command
            await Mediator.Send(command);
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> Unban(string userId)
        {
            UserUnbanCommand command = new() { Id = userId };
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

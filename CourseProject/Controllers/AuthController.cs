using CourseProject.Application.CQRS.Commands;
using CourseProject.Domain.Entities.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator Mediator;

        public AuthController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IActionResult Login() => View(new UserLoginDto());

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginCommand command)
        {
            var test = command;
            var result = await Mediator.Send(command);
            if (!result.Result)
            {
                TempData["Error"] = result.Error;
                return View(new UserLoginDto());
            };
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register() => View(new UserRegisterDto());

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Result == false)
            {
                TempData["Error"] = result.Error;
                return View(new UserRegisterDto());
            }
            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut(UserLogOutCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

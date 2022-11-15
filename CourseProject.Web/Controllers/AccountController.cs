using CourseProject.Application.CQRS.Commands.Account;
using CourseProject.Domain.Entities.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator Mediator;


        public AccountController(IMediator mediator)
        {
            Mediator = mediator;
        }


        [HttpGet]
        public IActionResult Login() => View(new AccountLoginDto());

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginCommand command)
        {
            var result = await Mediator.Send(command);
            // think about how to add cookie logic instead of Tempdata
            if (result.Result)
            {
                Response.Cookies.Append("userId", result.UserId);
                Response.Cookies.Append("userName", result.UserName);
            }
            //TempData["userId"] = result.UserId;
            if (!result.Result)
            {
                TempData["Error"] = result.Error;
                return View(new AccountLoginDto());
            };
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View(new AccountRegisterDto());

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Result == false)
            {
                TempData["Error"] = result.Error;
                return View(new AccountRegisterDto());
            }
            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut(AccountLogOutCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeTheme(string name)
        {
            AccountChangeThemeCommand command = new() { Name = name };
            await Mediator.Send(command);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

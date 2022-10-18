using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Web.Controllers
{
    public class TagsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

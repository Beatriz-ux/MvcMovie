using Microsoft.AspNetCore.Mvc;

namespace MvcMovie;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Welcome()
    {
        return View();
    }


}

using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers;

public class OpenController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
